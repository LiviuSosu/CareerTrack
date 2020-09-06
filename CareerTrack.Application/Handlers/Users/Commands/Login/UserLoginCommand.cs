using CareerTrack.Common;
using CareerTrack.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CareerTrack.Application.Handlers.Users.Commands.Login
{
    public class UserLoginCommand : IRequest<LoginResponseDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public JWTConfiguration JWTConfiguration  { get; set; }

        public UserManager<User> UserManager { get; set; }
    }
}
