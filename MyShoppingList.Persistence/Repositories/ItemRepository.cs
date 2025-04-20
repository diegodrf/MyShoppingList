using Microsoft.EntityFrameworkCore;
using MyShoppingList.Application.Ports.Secondary;
using MyShoppingList.Domain.Entities;

namespace MyShoppingList.Persistence.Repositories;
public class ItemRepository : IItemRepository
{
    private readonly MyShoppingListDbContext _dbContext;

    public ItemRepository(MyShoppingListDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Item> CreateAsync(Item item, CancellationToken cancellationToken)
    {
        await _dbContext.Items.AddAsync(item, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return item;
    }

    public Task<Item?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _dbContext.Items
            .AsSplitQuery()
            .Include(i => i.ItemGroups.Where(x => x.Group!.RemovedAt == null))
            .ThenInclude(g => g.Group)
            .Where(i => i.RemovedAt == null)
            .Where(i => i.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Item> UpdateAsync(Item item, CancellationToken cancellationToken)
    {
        _dbContext.Items.Update(item);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return item;
    }
}
