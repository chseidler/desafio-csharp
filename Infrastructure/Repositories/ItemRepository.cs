using Domain.Entity;
using Domain.Repository;

namespace Infrastructure.Repositories;

public class ItemRepository : IItemRepository
{
    public Task<IReadOnlyList<ItemDomain>> GetAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
