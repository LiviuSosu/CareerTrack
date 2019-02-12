using CareerTrack.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CareerTrack.Persistance
{
    public class CareerTrackInitializer
    {
        private readonly Dictionary<int, User> Employees = new Dictionary<int, User>();

        public static void Initialize(CareerTrackDbContext context)
        {
            var initializer = new CareerTrackInitializer();
            initializer.SeedEverything(context);
        }

        public void SeedEverything(CareerTrackDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return; // Db has been seeded
            }

            SeedUsers(context);
        }

        public void SeedUsers(CareerTrackDbContext context)
        {
            //var users = new[]
            //{
            //    new User{ },
            //    new User{ }
            //};

            //context.Users.AddRange(users);

            //context.SaveChanges();
        }
    }
}
