using CareerTrack.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CareerTrack.Persistance
{
    public class CareerTrackDbContext : IdentityDbContext<User>
    {
        public CareerTrackDbContext(DbContextOptions<CareerTrackDbContext> options)
            : base(options)
        {
        }

        public override DbSet<User>  Users { get; set; } //Possible bug
        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source = DESKTOP-M80MDUC;Initial Catalog=CareerTrack;Integrated Security = True;",
            //    x => x.MigrationsAssembly("CareerTrack.Migrations"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
