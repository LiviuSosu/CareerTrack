using CareerTrack.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;

namespace CareerTrack.Persistance.Repository.UserTokenRepository
{
    public class UserTokenRepository : RepositoryBase<UserToken>, IUserTokenRepository
    {
        public UserTokenRepository(CareerTrackDbContext careerTrackDbContext) : base(careerTrackDbContext)
        {
        }
    }
}
