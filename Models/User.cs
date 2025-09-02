using System;

namespace MyBlazorAppSourse.Models
{
    public class User : EditModel
    {
        public Guid Id { get; set; }
        public bool IsSelected { get; set; }
    }
}