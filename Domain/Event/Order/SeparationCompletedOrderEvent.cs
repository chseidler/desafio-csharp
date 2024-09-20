using MediatR;

namespace Domain.Event.Order;

public class SeparationCompletedOrderEvent : INotification
{
    public Guid OrderId { get; private set; }
    public DateTime SeparationCompletedAt { get; private set; }

    public SeparationCompletedOrderEvent(Guid orderId)
    {
        OrderId = orderId;
        SeparationCompletedAt = DateTime.Now;
    }
}
