namespace MyShoppingList.Domain.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? RemovedAt { get; set; }

        public List<ItemGroup> ItemGroups { get; set; } = [];
    }
}
