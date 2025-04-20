using MyShoppingList.Domain.Entities;

namespace MyShoppingList.Application.Ports.Secondary;
public interface IItemRepository
{
    Task<Item> CreateAsync(Item item, CancellationToken cancellationToken);
    Task<Item?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Item> UpdateAsync(Item item, CancellationToken cancellationToken);
}
