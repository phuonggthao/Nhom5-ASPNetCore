using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Entity
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data cho bảng Class
            modelBuilder.Entity<Class>().HasData(
                new Class { Id = 1, Name = "Class A" },
                new Class { Id = 2, Name = "Class B" }
            );

            // Seed data cho bảng Student
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "Vien Xuan Quy", DateOfBirth = new DateTime(2002, 5, 1), ClassId = 1 },
                new Student { Id = 2, Name = "Truong Thi Anh", DateOfBirth = new DateTime(2001, 10, 20), ClassId = 2 }
            );
        }
    }
}

