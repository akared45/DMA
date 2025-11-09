using Microsoft.EntityFrameworkCore;

namespace CodeFirstClean.Models
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }

        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Ward> Wards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Nurse>()
                .HasOne(n => n.Ward)
                .WithMany(w => w.Nurses)
                .HasForeignKey(n => n.WardId);
        }
    }
}
