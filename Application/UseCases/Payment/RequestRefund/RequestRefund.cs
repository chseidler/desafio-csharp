using Application.UseCases.Payment.PaymentMethod;
using Domain.Entity;
using Domain.Enum;
using Domain.Event;
using Domain.Repository;
using MediatR;

namespace Application.UseCases.Payment.RequestRefund;

public class RequestRefund : IRequestRefund
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMediator _mediator;

    public RequestRefund(IOrderRepository orderRepository, IPaymentRepository paymentRepository, IMediator mediator)
    {
        _orderRepository = orderRepository;
        _paymentRepository = paymentRepository;
        _mediator = mediator;
    }

    public async Task<RequestRefundOutput> Handle(RequestRefundInput request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null)
            throw new KeyNotFoundException($"Order with ID {request.OrderId} not found.");

        var oldOrderState = order.State;

        order.RequestRefund();
        await DomainEvents.DispatchNotifications(_mediator);

        var payment = await _paymentRepository.GetByOrderIdAsync(order.Id, cancellationToken);
        var paymentService = PaymentFactory.CreatePayment(payment.Method);

        if (paymentService.CanRefund())
        {
            paymentService.Refund();
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

        return new RequestRefundOutput(paymentService.CanRefund());
    }
}
