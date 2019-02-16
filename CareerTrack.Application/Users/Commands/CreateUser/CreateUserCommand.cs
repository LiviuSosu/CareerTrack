using CareerTrack.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;

namespace CareerTrack.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest
    {
        public string UserName { get; set; }
        public UserManager<User> UserManager { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
    }
}
