using Domain.Enum;
using Domain.Event;
using Domain.Event.Notification;

namespace Domain.Entity;

public class OrderDomain
{
    public Guid Id { get; private set; }
    public List<ItemDomain> Items { get; private set; }
    public decimal Total { get; private set; }
    public OrderStateEnum State { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Guid CustomerId { get; private set; }

    public OrderDomain(Guid customerId)
    {
        Id = Guid.NewGuid();
        Items = [];
        CreatedAt = DateTime.Now;
        CustomerId = customerId;
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

    public void Create(List<(ItemDomain item, uint quantity)> items)
    {
        if (items.Any(i => i.quantity == 0))
            throw new InvalidOperationException("Order can only be created if all items have more than 1 unit.");
        ValidateItemsStock(items);

        Items = items.Select(i => i.item).ToList();
        CalculateTotalAmount(items);
        State = OrderStateEnum.AguardandoProcessamento;

        DomainEvents.Raise(new OrderHasChangedNotificationEvent(Id, State, CustomerId));
    }

    public void Cancel()
    {
        if (State != OrderStateEnum.AguardandoProcessamento)
            throw new InvalidOperationException("Order can only be canceled if it's in 'Aguardando Processamento' state.");

        State = OrderStateEnum.Cancelado;
        DomainEvents.Raise(new OrderHasChangedNotificationEvent(Id, State, CustomerId));
    }

    public void StartPaymentProcess()
    {
        if (State != OrderStateEnum.AguardandoProcessamento)
            throw new InvalidOperationException("Order must be in 'Aguardando Processamento' to start payment.");

        State = OrderStateEnum.ProcessandoPagamento;
        DomainEvents.Raise(new OrderHasChangedNotificationEvent(Id, State, CustomerId));
    }

    public void CompletePayment(decimal finalAmount)
    {
        Total = finalAmount;

        State = OrderStateEnum.PagamentoConcluido;
        DomainEvents.Raise(new OrderHasChangedNotificationEvent(Id, State, CustomerId));
    }

    public void FailPayment()
    {
        State = OrderStateEnum.Cancelado;
        DomainEvents.Raise(new OrderHasChangedNotificationEvent(Id, State, CustomerId));
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
