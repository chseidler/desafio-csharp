using Domain.Enum;

namespace Domain.Entity;

public class OrderDomain
{
    public Guid Id { get; private set; }
    public List<ItemDomain> Items { get; private set; }
    public decimal Total { get; private set; }
    public OrderStateEnum State { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public OrderDomain(List<ItemDomain> items)
    {
        Id = Guid.NewGuid();
        Items = items ?? throw new ArgumentNullException(nameof(items));
        CalculateTotalAmout();
        State = OrderStateEnum.AguardandoProcessamento;
        CreatedAt = DateTime.Now;
    }

    public void CalculateTotalAmout()
    {
        Total = Items.Sum(i => i.PriceWithDiscount());
    }

    public void Cancel()
    {
        // TODO: Emitir evento? Condiçao?
    }

    public void ConfirmPayment()
    {
        // TODO: Emitir evento? Condiçao?
    }

    public void FinalizePayment()
    {
        // TODO: Emitir evento? Condiçao?
    }

    public void Separate()
    {
        // TODO: Emitir evento? Condiçao?
    }

    public void Conclude()
    {
        // TODO: Emitir evento? Condiçao?
    }
}
