namespace MyShoppingList.Application.Responses;
public abstract class ItemResponseBase
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Done { get; set; }
}

public class ReadItemResponse : ItemResponseBase
{
}
