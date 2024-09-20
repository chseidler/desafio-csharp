namespace Domain.Entity;

public class ItemDomain
{
    public Guid Id { get; private set; }
    public decimal Price { get; private set; }

    public ItemDomain(Guid itemId, decimal price)
    {
        Id = itemId;
        Price = price;
    }

    //public decimal PriceWithDiscount()
    //{
    //    decimal discount = ApplyDiscount();
    //}

    private static decimal ApplyDiscount()
    {
        // TODO: aplicar logica desconto
        return 1m;
    }
}
