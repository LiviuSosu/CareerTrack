using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CareerTrack.WebApi
{
    public class SeedDatabase
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<CareerTrackDbContext>();

            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            context.Database.EnsureCreated();

            if (!context.Roles.Any())
            {
                var roles = new[]
                {
                    new Role
                    {
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        Id = Guid.NewGuid(),
                        Name = "Admin"
                    },
                    new Role
                    {
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        Id = Guid.NewGuid(),
                        Name = "StandardUser"
                    }
                };

                context.Roles.AddRange(roles);

                User adminUser = new User
                {
                    Id = Guid.NewGuid(),
                    Email = "admin@b.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "admin2",
                };
                context.Users.AddAsync(adminUser);
                userManager.CreateAsync(adminUser, "Password@123");

                var standardUser = new User
                {
                    Id = Guid.NewGuid(),
                    Email = "std@user.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "Stduser",
                };
                context.Users.AddAsync(standardUser);
                userManager.CreateAsync(standardUser, "StdPassword@123");

                var identityAdminRole = new IdentityUserRole<Guid>
                {
                    RoleId = roles[0].Id,
                    UserId = adminUser.Id
                };

                context.UserRoles.AddAsync(identityAdminRole);

                var identityStandaerdUserRole = new IdentityUserRole<Guid>
                {
                    RoleId = roles[1].Id,
                    UserId = standardUser.Id
                };

                context.UserRoles.AddAsync(identityStandaerdUserRole);
                context.SaveChanges();
            }
        }
    }
}