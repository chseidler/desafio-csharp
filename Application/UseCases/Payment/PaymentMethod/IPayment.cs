using Domain.Entity;

namespace Application.UseCases.Payment.PaymentMethod;

public interface IPayment
{
    (bool IsSuccessful, decimal FinalAmount) ProcessPayment(OrderDomain order);
    bool CanRefund();
    void Refund();
}
