using MyShoppingList.Domain.Entities;

namespace MyShoppingList.Application.Ports.Secondary;
public interface IGroupRepository
{
    Task<Group> CreateAsync(Group group, CancellationToken cancellationToken);
    Task<Group?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<Group>> GetAllAsync(CancellationToken cancellationToken);
    Task UpdateAsync(Group group, CancellationToken cancellationToken);
    Task RemoveItemAsync(int groupId, int ItemId, CancellationToken cancellationToken);
}
