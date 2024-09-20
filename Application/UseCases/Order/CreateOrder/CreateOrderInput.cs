using MediatR;

namespace Application.UseCases.Order.CreateOrder;

public record CreateOrderInput(Guid CustomerId, List<ItemsInput> Items) : IRequest<CreateOrderOutput>;

public record ItemsInput(Guid Id, uint Quantity);