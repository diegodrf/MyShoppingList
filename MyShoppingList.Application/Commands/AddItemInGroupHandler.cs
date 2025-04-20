using MyShoppingList.Application.Abstractions;
using MyShoppingList.Application.Ports.Secondary;
using MyShoppingList.Application.Responses;

namespace MyShoppingList.Application.Commands;
public class AddItemInGroupHandler : IHandler<AddItemInGroupCommand, ReadGroupResponse?>
{
    private readonly IGroupRepository _groupRepository;

    public AddItemInGroupHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<ReadGroupResponse?> HandleAsync(AddItemInGroupCommand command, CancellationToken cancellationToken)
    {
        await _groupRepository.AddItemAsync(command.Id, command.GroupId, cancellationToken);
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
            Items = [.. group.Items.Select(x => new ReadItemResponse
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
