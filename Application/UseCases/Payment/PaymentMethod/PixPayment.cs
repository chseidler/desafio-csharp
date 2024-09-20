using Domain.Entity;

namespace Application.UseCases.Payment.PaymentMethod;

public class PixPayment : IPayment
{
    public (bool IsSuccessful, decimal FinalAmount) ProcessPayment(OrderDomain order)
    {
        return (true, order.Total * 0.95m);
    }

    public bool CanRefund() => false;

    public void Refund()
    {
        throw new InvalidOperationException("Cant`t refund Pix.");
    }
}
