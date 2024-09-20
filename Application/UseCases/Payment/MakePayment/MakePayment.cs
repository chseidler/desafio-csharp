using Application.UseCases.Payment.PaymentMethod;
using Domain.Entity;
using Domain.Enum;
using Domain.Event;
using Domain.Repository;
using FluentResults;
using MediatR;

namespace Application.UseCases.Payment.MakePayment;

public class MakePayment : IMakePayment
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMediator _mediator;

    public MakePayment(IOrderRepository orderRepository, IPaymentRepository paymentRepository, IMediator mediator)
    {
        _orderRepository = orderRepository;
        _paymentRepository = paymentRepository;
        _mediator = mediator;
    }

    public async Task<Result<MakePaymentOutput>> Handle(MakePaymentInput request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

            if (order is null)
                return Result.Fail($"Order with ID {request.OrderId} not found.");

            if (order.State != OrderStateEnum.AguardandoProcessamento)
                return Result.Fail("Order must be in 'Aguardando Processamento' state to process payment.");

            order.StartPaymentProcess();
            await DomainEvents.DispatchNotifications(_mediator);

            var paymentService = PaymentFactory.CreatePayment(request.Method);
            var (paymentSuccessful, finalAmout) = paymentService.ProcessPayment(order);

            if (paymentSuccessful)
                order.CompletePayment(finalAmout);
            else
                order.FailPayment();
            await DomainEvents.DispatchNotifications(_mediator);

            var paymentStatus = paymentSuccessful ? PaymentStatusEnum.Aprovado : PaymentStatusEnum.Reprovado;
            var payment = new PaymentDomain(order.Id, finalAmout, request.Method, paymentStatus);

            await _orderRepository.UpdateAsync(order, cancellationToken);
            await _paymentRepository.SaveAsync(payment, cancellationToken);

            return Result.Ok(new MakePaymentOutput(order.Id, paymentSuccessful));
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
