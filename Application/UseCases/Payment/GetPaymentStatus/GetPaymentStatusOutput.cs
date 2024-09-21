using Domain.Enum;

namespace Application.UseCases.Payment.GetPaymentStatus;

public record GetPaymentStatusOutput(PaymentStatusEnum Status);