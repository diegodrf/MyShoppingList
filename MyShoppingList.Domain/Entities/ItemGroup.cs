namespace MyShoppingList.Domain.Entities;
public class ItemGroup
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public int GroupId { get; set; }

    public List<Item> Items { get; set; } = [];
    public List<Group> Groups { get; set; } = [];
}
