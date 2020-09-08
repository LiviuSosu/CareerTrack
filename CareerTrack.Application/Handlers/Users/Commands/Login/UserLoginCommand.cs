using CareerTrack.Common;
using CareerTrack.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CareerTrack.Application.Handlers.Users.Commands.Login
{
    public class UserLoginCommand : UserCommandRequestBase, IRequest<LoginResponseDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public JWTConfiguration JWTConfiguration  { get; set; }
    }
}
