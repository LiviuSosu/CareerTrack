using CareerTrack.Domain.Entities;
using CareerTrack.Services.TokenManager;
using Microsoft.AspNetCore.Identity;

namespace CareerTrack.Application.Handlers.Users.Commands
{
    public class UserCommandRequestBase
    {
        public string Username { get; set; }
        public UserManager<User> UserManager { get; set; }
        public ITokenManager TokenManager { get; set; }
        public IJwtHandler JwtHandler { get; set; }
    }
}
