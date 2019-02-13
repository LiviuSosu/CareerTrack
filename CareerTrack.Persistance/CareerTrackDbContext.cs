using CareerTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CareerTrack.Persistance
{
    public class CareerTrackDbContext : DbContext
    {
        public CareerTrackDbContext(DbContextOptions<CareerTrackDbContext> options)
   : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
