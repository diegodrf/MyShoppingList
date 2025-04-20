using MyShoppingList.Application.Abstractions;
using MyShoppingList.Application.Ports.Secondary;
using MyShoppingList.Application.Responses;
using MyShoppingList.Domain.Entities;

namespace MyShoppingList.Application.Commands;
public class CreateItemHandler : IHandler<CreateItemCommand, ReadItemResponse>
{
    private readonly IItemRepository _itemRepository;
    public CreateItemHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }
    public async Task<ReadItemResponse> HandleAsync(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var newItem = await _itemRepository.CreateAsync(new Item { Name = request.Name }, cancellationToken);
        var item = await _itemRepository.GetByIdAsync(newItem.Id, cancellationToken);

        return new ReadItemResponse
        {
            Id = item!.Id,
            Name = item.Name,
            CreatedAt = item.CreatedAt,
            Done = item.Groups.First().Completed_At != null,
        };
    }
}

public class CreateItemCommand : IRequest<ReadItemResponse>
{
    public required string Name { get; set; }
    public required int GroupId { get; set; }
}
