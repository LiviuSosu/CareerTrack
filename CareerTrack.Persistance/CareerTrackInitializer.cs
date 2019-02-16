using CareerTrack.Domain.Entities;
using System.Collections.Generic;

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
            //context.Database.EnsureCreated();

            //if (!context.Roles.Any())
            //{
            //    SeedRoles(context);
            //}
        }

        public void SeedRoles(CareerTrackDbContext context)
        {
        }
    }
}
