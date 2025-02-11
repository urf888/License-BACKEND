using LoginAPI.Models;
using LoginAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace LoginAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<LoginLog> LoginLogs { get; set; }
    }
}
