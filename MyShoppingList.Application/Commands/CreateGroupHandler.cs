using MyShoppingList.Application.Abstractions;
using MyShoppingList.Application.Ports.Secondary;
using MyShoppingList.Application.Responses;
using MyShoppingList.Domain.Entities;

namespace MyShoppingList.Application.Commands;

public class CreateGroupHandler : IHandler<CreateGroupCommand, CreateGroupResponse>
{
    private readonly IGroupRepository _repository;

    public CreateGroupHandler(IGroupRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateGroupResponse> HandleAsync(CreateGroupCommand command, CancellationToken cancellationToken)
    {
        var group = await _repository.CreateAsync(new Group { Name = command.Name }, cancellationToken);
        return new CreateGroupResponse
        {
            Id = group.Id,
            Name = group.Name,
            CreatedAt = group.CreatedAt
        };
    }
}

public class CreateGroupCommand : IRequest<CreateGroupResponse>
{
    public required string Name { get; set; }
}
