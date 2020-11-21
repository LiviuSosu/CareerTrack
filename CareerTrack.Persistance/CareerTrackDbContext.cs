using CareerTrack.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace CareerTrack.Persistance
{
    public class CareerTrackDbContext : IdentityDbContext<User, Role, Guid>
    {
        public CareerTrackDbContext(DbContextOptions<CareerTrackDbContext> options)
            : base(options)
        {
        }

        public override DbSet<User>  Users { get; set; } //Possible bug
        public override DbSet<Role> Roles { get; set; } //Possible bug

        public override DbSet<IdentityUserRole<Guid>> UserRoles { get; set; } //Possible bug
        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.ToTable("Users");
            });

            modelBuilder.Entity<UserToken>(b =>
            {
                b.ToTable("UserTokens");
            });

            modelBuilder.Entity<Role>(b =>
            {
                b.ToTable("Roles");
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>(b =>
            {
                b.ToTable("UserRoles");
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
