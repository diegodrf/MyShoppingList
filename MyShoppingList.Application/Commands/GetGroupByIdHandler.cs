using MyShoppingList.Application.Abstractions;
using MyShoppingList.Application.Ports.Secondary;
using MyShoppingList.Application.Responses;

namespace MyShoppingList.Application.Commands;
public class GetGroupByIdHandler : IHandler<GetGroupByIdCommand, ReadGroupResponse?>
{
    private readonly IGroupRepository _repository;
    public GetGroupByIdHandler(IGroupRepository repository)
    {
        _repository = repository;
    }
    public async Task<ReadGroupResponse?> HandleAsync(GetGroupByIdCommand command, CancellationToken cancellationToken)
    {
        var group = await _repository.GetByIdAsync(command.Id, cancellationToken);

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
                Id = x.Item.Id,
                Name = x.Item.Name,
                Done = x.Completed_At != null,
                CreatedAt = x.Item.CreatedAt
            })]
        };
    }
}

public class GetGroupByIdCommand : IRequest<ReadGroupResponse?>
{
    public int Id { get; set; }
}
