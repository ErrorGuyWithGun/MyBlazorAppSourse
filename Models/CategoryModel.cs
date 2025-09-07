namespace MyBlazorAppSourse.Models
{
    public class CategoryModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<InventoryModel> Inventories { get; set; }
    }
}
