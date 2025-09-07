namespace MyBlazorAppSourse.Models
{ 
    public class UserDTOModel
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
        public string? Roles { get; set; }
    }
}
