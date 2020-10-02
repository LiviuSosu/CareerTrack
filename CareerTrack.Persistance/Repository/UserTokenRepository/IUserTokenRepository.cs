using Microsoft.AspNetCore.Identity;
using System;

namespace CareerTrack.Persistance.Repository.UserTokenRepository
{
    public interface IUserTokenRepository : IRepositoryBase<IdentityUserToken<Guid>>
    {
    }
}
