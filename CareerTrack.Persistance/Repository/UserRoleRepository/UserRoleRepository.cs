using Microsoft.AspNetCore.Identity;
using System;

namespace CareerTrack.Persistance.Repository.UserRoleRepository
{
    public class UserRoleRepository : RepositoryBase<IdentityUserRole<Guid>>, IUserRoleRepository
    {
        public UserRoleRepository(CareerTrackDbContext careerTrackDbContext) : base(careerTrackDbContext)
        {
        }
    }
}
