using Domain.Repository;

namespace Application.UseCases.Order.GetOrderStatus;

public class GetOrderStatus : IGetOrderStatus
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderStatus(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<GetOrderStatusOutput> Handle(GetOrderStatusInput request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null)
            throw new KeyNotFoundException($"Order with ID {request.OrderId} not found.");

        var orderOutput = new GetOrderStatusOutput(
            order.Id,
            order.State,
            order.Total,
            order.Items.Select(i => new ItemOutput(i.Id, i.Name, i.Price)).ToList());

        return orderOutput;
    }
}
