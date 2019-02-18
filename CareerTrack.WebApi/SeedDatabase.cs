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
                    new IdentityRole
                    {
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        Id = Guid.NewGuid().ToString(),
                        Name = "Admin"
                    },
                    new IdentityRole
                    {
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        Id = Guid.NewGuid().ToString(),
                        Name = "StandardUser"
                    }
                };

                context.Roles.AddRangeAsync(roles);

                IdentityRoleClaim<string> identityRoleClaim = new IdentityRoleClaim<string>
                {
                    ClaimType = "AddArticles",
                    ClaimValue = "Add articles",
                    RoleId = roles[0].Id
                };
                context.RoleClaims.AddAsync(identityRoleClaim);

                User user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = Guid.NewGuid(),
                    Email = "admin@b.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "admin",
                };
                context.Users.AddAsync(user);
                userManager.CreateAsync(user, "Password@123");

                IdentityUserRole<string> identityUserRole = new IdentityUserRole<string>
                {
                    RoleId = roles[0].Id,
                    UserId = user.Id
                };

                context.UserRoles.AddAsync(identityUserRole);

                var userClaim = new IdentityUserClaim<string>
                {
                    UserId = user.Id,
                    ClaimType = "AddArticles",
                    ClaimValue = "Add Articles"
                };

                context.UserClaims.AddAsync(userClaim);
            }
        }
    }
}
