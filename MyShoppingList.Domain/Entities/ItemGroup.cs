namespace MyShoppingList.Domain.Entities;
public class ItemGroup
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public int GroupId { get; set; }
    public DateTime? Completed_At { get; set; }

    public Item? Item { get; set; }
    public Group? Groups { get; set; }
}
