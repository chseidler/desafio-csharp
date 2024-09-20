using Domain.Entity;

namespace Domain.Repository;

public interface IOrderRepository
{
    Task SaveAsync(OrderDomain order, CancellationToken cancellationToken);
}
