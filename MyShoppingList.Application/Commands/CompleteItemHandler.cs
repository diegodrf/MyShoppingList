using MyShoppingList.Application.Abstractions;
using MyShoppingList.Application.Ports.Secondary;
using MyShoppingList.Application.Responses;

namespace MyShoppingList.Application.Commands;
public class CompleteItemHandler : IHandler<CompleteItemCommand, ReadItemResponse?>
{
    private readonly IItemRepository _itemRepository;
    public CompleteItemHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<ReadItemResponse?> HandleAsync(CompleteItemCommand command, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetByIdAsync(command.Id, cancellationToken);
        
        if (item == null)
        {
            return null;
        }

        item.Groups
            .First(x => x.GroupId == command.GroupId)
            .Completed_At = DateTime.UtcNow;

        await _itemRepository.UpdateAsync(item, cancellationToken);

        return new ReadItemResponse
        {
            Id = item.Id,
            Name = item.Name,
            CreatedAt = item.CreatedAt,
            Done = true,
        };
    }
}

public class CompleteItemCommand : IRequest<ReadItemResponse?>
{
    public required int Id { get; set; }
    public required int GroupId { get; set; }
}
