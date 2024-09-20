using Domain.Entity;
using Domain.Event;
using Domain.Repository;
using MediatR;

namespace Application.UseCases.Order.CreateOrder;

public class CreateOrder : ICreateOrder
{
    private readonly IItemRepository _itemRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IMediator _mediator;

    public CreateOrder(IItemRepository itemRepository, IOrderRepository orderRepository, IMediator mediator)
    {
        _itemRepository = itemRepository;
        _orderRepository = orderRepository;
        _mediator = mediator;
    }

    public async Task<CreateOrderOutput> Handle(CreateOrderInput request, CancellationToken cancellationToken)
    {
        var items = await _itemRepository.GetItemsByIdsAsync(request.Items.Select(i => i.Id).ToList(), cancellationToken);

        var orderItems = request.Items
            .Select(i => (item: items.FirstOrDefault(x => x.Id == i.Id), quantity: i.Quantity))
            .Where(i => i.item is not null)
            .ToList();

        var order = new OrderDomain(request.CustomerId);
        order.Create(orderItems!);

        await _orderRepository.SaveAsync(order, cancellationToken);
        await DomainEvents.DispatchNotifications(_mediator);

        return new CreateOrderOutput(order.Id, order.Total);
    }
}
