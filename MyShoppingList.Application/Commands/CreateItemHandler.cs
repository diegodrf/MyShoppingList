using MyShoppingList.Application.Abstractions;
using MyShoppingList.Application.Ports.Secondary;
using MyShoppingList.Application.Responses;
using MyShoppingList.Domain.Entities;

namespace MyShoppingList.Application.Commands;
public class CreateItemHandler : IHandler<CreateItemCommand, ReadItemResponse>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IItemRepository _itemRepository;

    public CreateItemHandler(IGroupRepository groupRepository, IItemRepository itemRepository)
    {
        _groupRepository = groupRepository;
        _itemRepository = itemRepository;
    }
    public async Task<ReadItemResponse> HandleAsync(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(request.GroupId, cancellationToken) 
            ?? throw new ArgumentException($"Group with id {request.GroupId} not found.");

        var _item = new Item 
        { 
            Name = request.Name,
            ItemGroups = [
                new() {
                    Group = group
            }]
        };

        var newItem = await _itemRepository.CreateAsync(_item, cancellationToken);
        var item = await _itemRepository.GetByIdAsync(newItem.Id, cancellationToken);

        return new ReadItemResponse
        {
            Id = item!.Id,
            Name = item.Name,
            CreatedAt = item.CreatedAt,
            Done = item.ItemGroups.First().Completed_At != null,
        };
    }
}

public class CreateItemCommand : IRequest<ReadItemResponse>
{
    public required string Name { get; set; }
    public required int GroupId { get; set; }
}
