using CareerTrack.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace CareerTrack.Persistance
{
    public class CareerTrackDbContext : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>,IdentityUserRole<Guid>,IdentityUserLogin<Guid>,IdentityRoleClaim<Guid>, UserToken>
                                       
    {
        public CareerTrackDbContext(DbContextOptions<CareerTrackDbContext> options)
            : base(options)
        {
        }

        public override DbSet<User>  Users { get; set; }
        public override DbSet<Role> Roles { get; set; } 

        public override DbSet<IdentityUserRole<Guid>> UserRoles { get; set; }
        public override DbSet<UserToken> UserTokens { get; set; }

        public DbSet<Article> Articles { get; set; }

        //https://stackoverflow.com/questions/65200896/how-to-update-database-via-migrations-when-i-have-two-or-more-config-files-wit/65200981#65200981

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
