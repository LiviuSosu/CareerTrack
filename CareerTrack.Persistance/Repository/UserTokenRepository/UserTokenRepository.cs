using CareerTrack.Domain.Entities;

namespace CareerTrack.Persistance.Repository.UserTokenRepository
{
    public class UserTokenRepository : RepositoryBase<UserToken>, IUserTokenRepository
    {
        public UserTokenRepository(CareerTrackDbContext careerTrackDbContext) : base(careerTrackDbContext)
        {
        }
    }
}
