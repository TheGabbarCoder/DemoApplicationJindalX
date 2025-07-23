using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Payers> Payers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<AgeingBucket> AgeingBuckets { get; set; }
        public DbSet<PriorityType> PriorityTypes { get; set; }
        public DbSet<PrioritySetup> PrioritySetups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed master data
            modelBuilder.Entity<Payers>().HasData(
                new Payers { Id = 1, Name = "Payer A" },
                new Payers { Id = 2, Name = "Payer B" }
            );
            modelBuilder.Entity<Location>().HasData(
                new Location { Id = 1, Name = "Location 1" },
                new Location { Id = 2, Name = "Location 2" }
            );
            modelBuilder.Entity<AgeingBucket>().HasData(
                new AgeingBucket { Id = 1, Name = "0-30 Days" },
                new AgeingBucket { Id = 2, Name = "31-60 Days" }
            );
            modelBuilder.Entity<PriorityType>().HasData(
                new PriorityType { Id = 1, Name = "High" },
                new PriorityType { Id = 2, Name = "Medium" },
                new PriorityType { Id = 3, Name = "Low" }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
