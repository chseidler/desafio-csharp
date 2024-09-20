using Domain.Entity;

namespace Application.UseCases.Payment.PaymentMethod;

public class DebitoPayment : IPayment
{
    public (bool IsSuccessful, decimal FinalAmount) ProcessPayment(OrderDomain order)
    {
        return (true, order.Total);
    }
}
