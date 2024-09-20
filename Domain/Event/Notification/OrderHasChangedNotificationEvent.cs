using Domain.Enum;
using MediatR;

namespace Domain.Event.Notification;

public class OrderHasChangedNotificationEvent : INotification
{
    public Guid OrderId { get; private set; }
    public OrderStateEnum OrderState { get; private set; }
    public string ClientEmail { get; private set; }
    public DateTime ChangedAt { get; private set; }

    public OrderHasChangedNotificationEvent(Guid orderId, OrderStateEnum orderState, string clientEmail)
    {
        OrderId = orderId;
        OrderState = orderState;
        ClientEmail = clientEmail;
        ChangedAt = DateTime.Now;
    }
}
