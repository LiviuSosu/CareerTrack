

namespace CareerTrack.Persistance
{
    public class CareerTrackInitializer
    {
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
