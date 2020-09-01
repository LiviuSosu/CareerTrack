using CareerTrack.Domain.Entities;

namespace CareerTrack.Persistance.Repository.UserRepository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(CareerTrackDbContext careerTrackDbContext) : base(careerTrackDbContext)
        {
        }
    }
}
