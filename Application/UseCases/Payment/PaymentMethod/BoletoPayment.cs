using Domain.Entity;

namespace Application.UseCases.Payment.PaymentMethod;

public class BoletoPayment : IPayment
{
    public (bool IsSuccessful, decimal FinalAmount) ProcessPayment(OrderDomain order)
    {
        return (true, order.Total);
    }

    public bool CanRefund() => false;

    public void Refund()
    {
        throw new InvalidOperationException("Cant`t refund Boleto.");
    }
}
