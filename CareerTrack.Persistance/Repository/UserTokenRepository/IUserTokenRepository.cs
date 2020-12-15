using CareerTrack.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;

namespace CareerTrack.Persistance.Repository.UserTokenRepository
{
    public interface IUserTokenRepository : IRepositoryBase<UserToken>
    {
    }
}
