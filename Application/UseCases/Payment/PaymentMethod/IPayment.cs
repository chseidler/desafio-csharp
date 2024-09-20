using Domain.Entity;

namespace Application.UseCases.Payment.PaymentMethod;

public interface IPayment
{
    Task<(bool IsSuccessful, decimal FinalAmount)> ProcessPayment(OrderDomain order);
    bool CanRefund();
    Task Refund(Guid paymentId);
}
