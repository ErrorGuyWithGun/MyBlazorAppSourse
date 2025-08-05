using System;

namespace MyBlazorAppSourse.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime LastSeen { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}