using System;

namespace MyBlazorAppSourse.Models
{
    public class User : EditModel
    {
        public string Id { get; set; }
        public bool IsSelected { get; set; }
    }
}