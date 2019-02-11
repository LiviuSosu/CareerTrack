using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerTrack.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest
    {
        public string Id { get; set; }
        //...
    }
}
