using CareerTrack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerTrack.Persistance.Repository.UserRoleRepository
{
    public class UserRoleRepository : RepositoryBase<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(CareerTrackDbContext careerTrackDbContext) : base(careerTrackDbContext)
        {
        }
    }
}
