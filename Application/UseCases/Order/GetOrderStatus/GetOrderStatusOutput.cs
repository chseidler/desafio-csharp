using Domain.Enum;

namespace Application.UseCases.Order.GetOrderStatus;

public record GetOrderStatusOutput(Guid Id, OrderStateEnum status, decimal amout, List<ItemOutput> items);