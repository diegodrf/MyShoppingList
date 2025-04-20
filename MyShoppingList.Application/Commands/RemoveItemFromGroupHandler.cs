using MyShoppingList.Application.Abstractions;
using MyShoppingList.Application.Ports.Secondary;
using MyShoppingList.Application.Responses;

namespace MyShoppingList.Application.Commands;
public class RemoveItemFromGroupHandler : IHandler<RemoveItemFromGroupCommand, ReadGroupResponse?>
{
    private readonly IGroupRepository _groupRepository;
    public RemoveItemFromGroupHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }
    public async Task<ReadGroupResponse?> HandleAsync(RemoveItemFromGroupCommand command, CancellationToken cancellationToken)
    {
        await _groupRepository.RemoveItemAsync(command.GroupId, command.ItemId, cancellationToken);
        var group = await _groupRepository.GetByIdAsync(command.GroupId, cancellationToken);
        if (group == null)
        {
            return null;
        }
        return new ReadGroupResponse
        {
            Id = group.Id,
            Name = group.Name,
            CreatedAt = group.CreatedAt,
            Items = [.. group.ItemGroups.Select(x => new ReadItemResponse
            {
                Id = x.ItemId,
                Name = x.Item!.Name,
                CreatedAt = x.Item.CreatedAt,
                Done = x.Completed_At != null,
            })]
        };
    }
}

public class RemoveItemFromGroupCommand : IRequest<ReadGroupResponse?>
{
    public required int ItemId { get; set; }
    public required int GroupId { get; set; }
}
