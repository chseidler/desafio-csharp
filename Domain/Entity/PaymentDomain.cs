using Domain.Enum;

namespace Domain.Entity;

public class PaymentDomain
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentMethodEnum Method { get; private set; }
    public PaymentStatusEnum Status { get; private set; }

    public PaymentDomain(Guid orderId, decimal amout, PaymentMethodEnum method, PaymentStatusEnum status)
    {
        Id = new Guid();
        OrderId = orderId;
        Amount = amout;
        Method = method;
        Status = status;
    }
}
