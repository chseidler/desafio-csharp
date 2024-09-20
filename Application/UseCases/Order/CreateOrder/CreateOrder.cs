using Domain.Entity;
using Domain.Repository;

namespace Application.UseCases.Order.CreateOrder;

public class CreateOrder : ICreateOrder
{
    private readonly IItemRepository _itemRepository;
    private readonly IOrderRepository _orderRepository;

    public CreateOrder(IItemRepository itemRepository, IOrderRepository orderRepository)
    {
        _itemRepository = itemRepository;
        _orderRepository = orderRepository;
    }

    public async Task<CreateOrderOutput> Handle(CreateOrderInput request, CancellationToken cancellationToken)
    {
        var items = await _itemRepository.GetItemsByIdsAsync(request.Items.Select(i => i.Id).ToList(), cancellationToken);

        var orderItems = request.Items
            .Select(i => (item: items.FirstOrDefault(x => x.Id == i.Id), quantity: i.Quantity))
            .ToList();

        var order = new OrderDomain(orderItems!);

        await _orderRepository.SaveAsync(order, cancellationToken);

        return new CreateOrderOutput(order.Id, order.Total);
    }
}
