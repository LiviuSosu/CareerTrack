using MediatR;
using System;

namespace CareerTrack.Application.Users.Commands.UpdateCustomer
{
    public class UpdateUserCommand : IRequest
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }
    }
}
