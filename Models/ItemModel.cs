namespace MyBlazorAppSourse.Models
{
    public class ItemModel
    {
        public Guid Id { get; set; }
        public Guid inventoryId { get; set; }

        public string? Title { get; set; }
        public string? Price { get; set; }
        public string? Description { get; set; }

        public bool isSelect { get; set; }
    }
}
