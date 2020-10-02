using Microsoft.AspNetCore.Identity;
using System;

namespace CareerTrack.Persistance.Repository.UserTokenRepository
{
    public class UserTokenRepository : RepositoryBase<IdentityUserToken<Guid>>, IUserTokenRepository
    {
        public UserTokenRepository(CareerTrackDbContext careerTrackDbContext) : base(careerTrackDbContext)
        {
        }
    }
}
