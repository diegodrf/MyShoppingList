using MyShoppingList.Application.Abstractions;
using MyShoppingList.Application.Ports.Secondary;
using MyShoppingList.Application.Responses;

namespace MyShoppingList.Application.Commands;
public class UncompleteItemHandler : IHandler<UncompleteItemCommand, ReadItemResponse?>
{
    private readonly IItemRepository _itemRepository;
    public UncompleteItemHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }
    public async Task<ReadItemResponse?> HandleAsync(UncompleteItemCommand command, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetByIdAsync(command.Id, cancellationToken);
        
        if (item == null)
        {
            return null;
        }
        
        item.ItemGroups
            .First(x => x.GroupId == command.GroupId)
            .Completed_At = null;
        
        await _itemRepository.UpdateAsync(item, cancellationToken);
        
        return new ReadItemResponse
        {
            Id = item.Id,
            Name = item.Name,
            CreatedAt = item.CreatedAt,
            Done = false,
        };
    }
}

public class UncompleteItemCommand : IRequest<ReadItemResponse?>
{
    public required int Id { get; set; }
    public required int GroupId { get; set; }
}
