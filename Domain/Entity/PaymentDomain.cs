using Domain.Enum;

namespace Domain.Entity;

public class PaymentDomain
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentMethodEnum Method { get; private set; }
    public bool IsApproved { get; private set; }

    public PaymentDomain(Guid orderId, decimal amout, PaymentMethodEnum method)
    {
        Id = new Guid();
        OrderId = orderId;
        Amount = amout;
        Method = method;
        IsApproved = false;
    }

    public void Process()
    {
        if (Method == PaymentMethodEnum.Pix)
            Amount = Amount * 0.90m;

        IsApproved = true;
    }

}
