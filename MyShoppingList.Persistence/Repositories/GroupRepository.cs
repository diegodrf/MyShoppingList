using Microsoft.EntityFrameworkCore;
using MyShoppingList.Application.Ports.Secondary;
using MyShoppingList.Domain.Entities;

namespace MyShoppingList.Persistence.Repositories;
public class GroupRepository : IGroupRepository
{
    private readonly MyShoppingListDbContext _db;
    public GroupRepository(MyShoppingListDbContext db)
    {
        _db = db;
    }

    public async Task<Group> CreateAsync(Group group, CancellationToken cancellationToken)
    {
        await _db.Groups.AddAsync(group, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        return group;
    }

    public Task<List<Group>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _db.Groups
            .AsSplitQuery()
            .Include(g => g.ItemGroups.Where(x => x.Item!.RemovedAt == null))
            .ThenInclude(i => i.Item)
            .Where(g => g.RemovedAt == null)
            .ToListAsync(cancellationToken);
    }

    public Task<Group?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _db.Groups
            .AsSplitQuery()
            .Include(g => g.ItemGroups.Where(x => x.Item!.RemovedAt == null))
            .ThenInclude(i => i.Item)
            .Where(g => g.RemovedAt == null)
            .Where(g => g.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task RemoveItemAsync(int groupId, int ItemId, CancellationToken cancellationToken)
    {
        await _db.ItemGroups
            .Where(x => x.GroupId == groupId)
            .Where(x => x.ItemId == ItemId)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public Task UpdateAsync(Group group, CancellationToken cancellationToken)
    {
        _db.Groups.Update(group);
        return _db.SaveChangesAsync(cancellationToken);
    }
}
