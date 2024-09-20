using Domain.Entity;
using Domain.Repository;

namespace Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    public Task SaveAsync(OrderDomain order, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
