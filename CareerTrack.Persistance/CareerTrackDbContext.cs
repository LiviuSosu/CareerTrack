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

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = EN614080\\SQLEXPRESS;Initial Catalog=CareerTrack;Integrated Security= True"
                //,x => x.MigrationsAssembly("ClassLibrary2")
                );

            base.OnConfiguring(optionsBuilder);
        }
    }
}
