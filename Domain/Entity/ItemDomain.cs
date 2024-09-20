namespace Domain.Entity;

public class ItemDomain
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public uint QuantityInStock { get; private set; }

    public ItemDomain(Guid itemId, string name, decimal price, uint quantityInStock)
    {
        Id = itemId;
        Name = name;
        Price = price;
        QuantityInStock = quantityInStock;
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
