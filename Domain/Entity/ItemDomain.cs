namespace Domain.Entity;

public class ItemDomain
{
    public Guid Id { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    public ItemDomain(Guid itemId, int quantity, decimal price)
    {
        Id = itemId;
        Quantity = quantity;
        Price = price;
    }

    public decimal PriceWithDiscount()
    {
        decimal discount = ApplyDiscount();
        return (Price * Quantity / discount);
    }

    private static decimal ApplyDiscount()
    {
        // TODO: aplicar logica desconto
        return 1m;
    }
}
