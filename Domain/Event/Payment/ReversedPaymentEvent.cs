using MediatR;

namespace Domain.Event.Payment;

public class ReversedPaymentEvent : INotification
{
    public Guid OrderId { get; private set; }
    public decimal AmountReversed { get; private set; }

    public ReversedPaymentEvent(Guid orderId, decimal amountReversed)
    {
        OrderId = orderId;
        AmountReversed = amountReversed;
    }
}
