using HW8.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HW8.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Article> Articles => Set<Article>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Employee> Employees => Set<Employee>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>().HasData(
                new Article { Id = 1, Title = "Welcome", Content = "First article" });

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Sample Product", Price = 100.00m });

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Name = "Admin User", Position = "Administrator" });
        }
    }
}
