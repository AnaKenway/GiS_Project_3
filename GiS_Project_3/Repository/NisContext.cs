using Microsoft.EntityFrameworkCore;
using GiS_Project_3.Models;

namespace GiS_Project_3.Repository
{
    public class NisContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrafficData>()
                .HasNoKey();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql("Server=localhost;Port=5432;Database=nis;User Id=postgres;Password=ADMIN;");
        }
        public DbSet<TrafficData> NisFCDTable { get; set; }

    }
}
