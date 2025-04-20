using MyShoppingList.Application.Abstractions;
using MyShoppingList.Application.Ports.Secondary;
using MyShoppingList.Application.Responses;
using MyShoppingList.Domain.Entities;

namespace MyShoppingList.Application.Commands;
public class AddItemInGroupHandler : IHandler<AddItemInGroupCommand, ReadGroupResponse?>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IItemRepository _itemRepository;

    public AddItemInGroupHandler(IGroupRepository groupRepository, IItemRepository itemRepository)
    {
        _groupRepository = groupRepository;
        _itemRepository = itemRepository;
    }

    public async Task<ReadGroupResponse?> HandleAsync(AddItemInGroupCommand command, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new ArgumentException($"Item with id {command.Id} not found.");

        var group = await _groupRepository.GetByIdAsync(command.GroupId, cancellationToken)
            ?? throw new ArgumentException($"Group with id {command.GroupId} not found.");

        group.ItemGroups.Add(new ItemGroup
        {
            Group = group,
            Item = item,
            Completed_At = null
        });

        await _groupRepository.UpdateAsync(group, cancellationToken);

        return new ReadGroupResponse
        {
            Id = group.Id,
            Name = group.Name,
            CreatedAt = group.CreatedAt,
            Items = [.. group.ItemGroups.Select(x => new ReadItemResponse
            {
                Id = x.Id,
                Name = x.Item!.Name,
                CreatedAt = x.Item.CreatedAt,
                Done = x.Completed_At != null,
            })]
        };
    }
}

public class AddItemInGroupCommand : IRequest<ReadGroupResponse?>
{
    public required int Id { get; set; }
    public required int GroupId { get; set; }
}
