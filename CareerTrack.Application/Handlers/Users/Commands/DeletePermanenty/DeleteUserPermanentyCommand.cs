using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerTrack.Application.Handlers.Users.Commands.DeletePermanenty
{
    public class DeleteUserPermanentyCommand : UserCommandRequestBase, IRequest
    {
        public Guid UserId { get; set; }
    }
}
