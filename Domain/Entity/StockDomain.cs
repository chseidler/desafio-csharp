namespace Domain.Entity;

public class StockDomain
{
    public Guid ItemId { get; private set; }
    public uint Quantity { get; private set; }

    public StockDomain(Guid itemId, uint quantity)
    {
        ItemId = itemId;
        Quantity = quantity;
    }
}
