﻿using MediatR;

namespace Domain.Event.Payment;

public class CompletedFailedEvent : INotification
{
    public Guid OrderId { get; private set; }
    public string FailureReason { get; private set; }

    public CompletedFailedEvent(Guid orderId, string failureReason)
    {
        OrderId = orderId;
        FailureReason = failureReason;
    }
}
