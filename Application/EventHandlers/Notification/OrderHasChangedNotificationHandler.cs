using Application.Interfaces;
using Domain.Event.Notification;
using MediatR;

namespace Application.EventHandlers.Notification;

public class OrderHasChangedNotificationHandler : INotificationHandler<OrderHasChangedNotificationEvent>
{
    private readonly IEmailService _emailService;

    public OrderHasChangedNotificationHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(OrderHasChangedNotificationEvent notification, CancellationToken cancellationToken)
    {
        var subject = $"Order {notification.OrderId} has changed!";
        var message = $"Your order is now in {notification.OrderState} state.";

        // TODO : buscar na base email do cliente pelo Id

        await _emailService.SendEmailAsync(notification.CustomerId.ToString(), subject, message);
    }
}
