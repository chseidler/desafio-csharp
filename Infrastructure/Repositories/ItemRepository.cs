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
        var items = new List<ItemDomain>
        {
            new(new Guid("2d4f68e9-e31d-4e1a-9038-2f2d6fb7839a"), "Item 1", 1000m, 1000),
            new(new Guid("91c2ca9f-a6a4-4bef-adb0-7fd8c7289a32"), "Item 2", 100m, 1000),
            new(new Guid("43716b09-36d7-444e-a8c7-8d46697b3aa3"), "Item 3", 10000m, 1000)
        };

        return Task.FromResult<IReadOnlyList<ItemDomain>>(items);
    }
}
