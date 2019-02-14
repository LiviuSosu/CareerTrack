using MediatR;
using System;

namespace CareerTrack.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest
    {
        public string UserName { get; set; }
    }
}
