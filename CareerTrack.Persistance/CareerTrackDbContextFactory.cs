using CareerTrack.Persistance.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CareerTrack.Persistance
{
    public class CareerTrackDbContextFactory : DesignTimeDbContextFactoryBase<CareerTrackDbContext>
    {
        protected override CareerTrackDbContext CreateNewInstance(DbContextOptions<CareerTrackDbContext> options)
        {
            return new CareerTrackDbContext(options);
        }
    }
}
