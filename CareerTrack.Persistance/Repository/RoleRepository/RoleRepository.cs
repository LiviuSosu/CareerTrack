using CareerTrack.Domain.Entities;

namespace CareerTrack.Persistance.Repository.RoleRepository
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(CareerTrackDbContext careerTrackDbContext) : base(careerTrackDbContext)
        {
        }
    }
}
