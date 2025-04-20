using MyShoppingList.Application.Abstractions;
using MyShoppingList.Application.Ports.Secondary;
using MyShoppingList.Application.Responses;

namespace MyShoppingList.Application.Commands;
public class GetAllGroupsHandler : IHandler<GetAllGroupsCommand, IReadOnlyList<ReadGroupResponse>>
{
    private readonly IGroupRepository _repository;

    public GetAllGroupsHandler(IGroupRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ReadGroupResponse>> HandleAsync(GetAllGroupsCommand command, CancellationToken cancellationToken)
    {
        var groups = await _repository.GetAllAsync(cancellationToken);
        return [.. groups.Select(group => new ReadGroupResponse
        {
            Id = group.Id,
            Name = group.Name,
            CreatedAt = group.CreatedAt,
            Items = [.. group.ItemGroups.Select(x => new ReadItemResponse
            {
                Id = x.Item.Id,
                Name = x.Item.Name,
                Done = x.Completed_At != null,
                CreatedAt = x.Item.CreatedAt
            })]
        })];
    }
}

public class GetAllGroupsCommand : IRequest<IReadOnlyList<ReadGroupResponse>>
{
}