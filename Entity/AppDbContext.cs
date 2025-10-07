using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Entity
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Class> Classes { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
    }
}
