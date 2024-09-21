namespace Application.UseCases.Payment.MakePayment;

public record MakePaymentOutput(Guid PaymentId, bool IsSucess);