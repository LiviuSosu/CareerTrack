using MediatR;
using System;

namespace CareerTrack.Application.Handlers.Users.Commands.Register
{
    public class UserRegisterCommand : UserCommandRequestBase, IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
