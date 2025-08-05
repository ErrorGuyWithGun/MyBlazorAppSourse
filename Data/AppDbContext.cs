using Microsoft.EntityFrameworkCore;
using MyBlazorAppSourse.Models;

namespace MyBlazorAppSourse.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}