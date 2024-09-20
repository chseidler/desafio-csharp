using Domain.Entity;
using Domain.Repository;

namespace Infrastructure.Repositories;

public class ItemRepository : IItemRepository
{
    public Task<IReadOnlyList<ItemDomain>> GetAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<ItemDomain>> GetItemsByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
