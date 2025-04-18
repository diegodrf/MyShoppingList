namespace MyShoppingList.Application.Responses;

public abstract class GroupResponseBase
{
    public DateTime CreatedAt { get; set; }
    public int Id { get; set; }
    public required string Name { get; set; }
}

public class CreateGroupResponse : GroupResponseBase
{
}

public class ReadGroupResponse : GroupResponseBase
{
    // TODO: Fix with a right type
    public IReadOnlyList<ReadItemResponse> Items { get; set; } = [];
}