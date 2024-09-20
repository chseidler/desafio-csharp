using Domain.Enum;
using MediatR;

namespace Domain.Event.Payment;

public class ProcessingPaymentEvent : INotification
{
    public Guid OrderId { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentMethodEnum PaymentMethod { get; private set; }

    public ProcessingPaymentEvent(Guid orderId, decimal amount, PaymentMethodEnum paymentMethod)
    {
        OrderId = orderId;
        Amount = amount;
        PaymentMethod = paymentMethod;
    }
}
