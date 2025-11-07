using Microsoft.EntityFrameworkCore;
using Token.Models;

namespace Token.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) 
        { }

        public DbSet<User> Users => Set<User>();
    }
}
