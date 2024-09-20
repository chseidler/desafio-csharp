using Application.Interfaces;
using Application.UseCases.Payment.PaymentMethod;
using Domain.Entity;
using Domain.Enum;
using Domain.Event;
using Domain.Repository;
using FluentResults;
using MediatR;

namespace Application.UseCases.Payment.RequestRefund;

public class RequestRefund : IRequestRefund
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMediator _mediator;
    private readonly IPaymentGateway _paymentGateway;

    public RequestRefund(IOrderRepository orderRepository, IPaymentRepository paymentRepository, IMediator mediator, IPaymentGateway paymentGateway)
    {
        _orderRepository = orderRepository;
        _paymentRepository = paymentRepository;
        _mediator = mediator;
        _paymentGateway = paymentGateway;
    }

    public async Task<Result<RequestRefundOutput>> Handle(RequestRefundInput request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

            if (order is null)
                return Result.Fail($"Order with ID {request.OrderId} not found.");

            var oldOrderState = order.State;

            order.RequestRefund();
            await DomainEvents.DispatchNotifications(_mediator);

            var payment = await _paymentRepository.GetByOrderIdAsync(order.Id, cancellationToken);
            var paymentService = PaymentFactory.CreatePayment(payment.Method, _paymentGateway);

            if (paymentService.CanRefund())
            {
                await paymentService.Refund(payment.Id);
                order.Refund();
                var paymentUpdate = new PaymentDomain(order.Id, order.Total, payment.Method, PaymentStatusEnum.Reembolsado);
                await _paymentRepository.SaveAsync(paymentUpdate, cancellationToken);
                await _orderRepository.UpdateAsync(order, cancellationToken);
            }
            else
            {
                order.NonRefundable(oldOrderState);
            }

            await DomainEvents.DispatchNotifications(_mediator);

            return Result.Ok(new RequestRefundOutput(paymentService.CanRefund()));
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
