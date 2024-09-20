namespace Application.Interfaces;

public interface IPaymentGateway
{
    Task<bool> ProcessPaymentAsync(decimal amount);
    Task RefundPaymentAsync(Guid paymentId);
}
