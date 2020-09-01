using CareerTrack.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CareerTrack.Application.Handlers.Users.Commands.Login
{
    public class UserLoginCommand : IRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public UserManager<User> userManager { get; set; }
    }
}
