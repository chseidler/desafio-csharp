using MediatR;

namespace Application.UseCases.Order.GetOrderStatus;

public record GetOrderStatusInput(Guid OrderId) : IRequest<GetOrderStatusOutput>;
