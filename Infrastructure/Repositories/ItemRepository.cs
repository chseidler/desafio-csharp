using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly DesafioDbContext _dbContext;

    public ItemRepository(DesafioDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<ItemDomain>> GetAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Items.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ItemDomain>> GetItemsByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
    {
        return await _dbContext.Items
            .AsNoTracking()
            .Where(item => ids.Contains(item.Id))
            .ToListAsync(cancellationToken);
    }
}
