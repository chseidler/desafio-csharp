using Domain.Entity;

namespace Domain.Repository;

public interface IItemRepository
{
    Task<IReadOnlyList<ItemDomain>> GetAsync(CancellationToken cancellationToken);
}
