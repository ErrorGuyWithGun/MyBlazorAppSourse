namespace MyBlazorAppSourse.Models
{
    public class DiscussionModel
    {
        public Guid Id { get; set; }
        public Guid inventoryId { get; set; }
        public string? userId { get; set; }
        public string? Text { get; set; }
    }
}
