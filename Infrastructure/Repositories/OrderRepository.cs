using Domain.Entity;
using Domain.Repository;

namespace Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    public Task<OrderDomain> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync(OrderDomain order, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task<OrderDomain> UpdateAsync(OrderDomain order, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
