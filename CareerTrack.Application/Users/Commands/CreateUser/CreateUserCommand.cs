using MediatR;
using System;

namespace CareerTrack.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
    }
}
