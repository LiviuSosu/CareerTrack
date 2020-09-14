using Microsoft.AspNetCore.Identity;
using System;

namespace CareerTrack.Persistance.Repository.UserRoleRepository
{
    public interface IUserRoleRepository : IRepositoryBase<IdentityUserRole<Guid>>
    {
    }
}
