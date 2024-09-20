using Domain.Enum;

namespace Domain.Entity;

public class OrderDomain
{
    public Guid Id { get; private set; }
    public List<ItemDomain> Items { get; private set; }
    public decimal Total { get; private set; }
    public OrderStateEnum State { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public OrderDomain(List<(ItemDomain item, uint quantity)> items)
    {
        ValidateItemsStock(items);

        Id = Guid.NewGuid();
        Items = items.Select(i => i.item).ToList();
        State = OrderStateEnum.AguardandoProcessamento;
        CreatedAt = DateTime.Now;

        CalculateTotalAmount(items);
    }

    private static void ValidateItemsStock(List<(ItemDomain item, uint quantity)> items)
    {
        if (items is null || items.Count == 0)
            throw new ArgumentException("Order must contain at least one item.", nameof(items));

        foreach (var (item, quantity) in items)
        {
            if (!item.CanFulfillOrder(quantity))
                throw new InvalidOperationException($"Not enough stock for item {item.Name}. Requested: {quantity}, Available: {item.QuantityInStock}");
        }
    }

    private void CalculateTotalAmount(List<(ItemDomain item, uint quantity)> items)
    {
        Total = items.Sum(i => i.item.Price * i.quantity);

        Total -= ApplyQuantityDiscount(items);
        Total -= ApplySeasonalDiscount(Total);
    }

    private static decimal ApplyQuantityDiscount(List<(ItemDomain item, uint quantity)> items)
    {
        decimal discount = 0;

        foreach (var item in items)
            discount += GetQuantityDiscount(item.quantity);

        return discount;
    }

    private static decimal GetQuantityDiscount(uint quantity)
    {
        if (quantity > 50) return 5m;
        if (quantity > 20) return 3m;
        if (quantity > 10) return 1m;
        return 0;
    }

    private static decimal ApplySeasonalDiscount(decimal total)
    {
        if (DateTime.Now.Month is 12)
            return total * 0.08m;
        return 0;
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
