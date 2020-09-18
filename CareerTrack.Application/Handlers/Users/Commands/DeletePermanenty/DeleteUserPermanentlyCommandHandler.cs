using CareerTrack.Application.Handlers.Articles;
using CareerTrack.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Handlers.Users.Commands.DeletePermanenty
{
    public class DeleteUserPermanentlyCommandHandler : BaseHandler<DeleteUserPermanentyCommand, Unit>, IRequestHandler<DeleteUserPermanentyCommand, Unit>
    {
        public DeleteUserPermanentlyCommandHandler(CareerTrackDbContext context) : base(context)
        {
        }

        public new async Task<Unit> Handle(DeleteUserPermanentyCommand request, CancellationToken cancellationToken)
        {
            var user  = await _repoWrapper.User.FindByIdAsync(request.UserId);
            await request.UserManager.DeleteAsync(user);

            return Unit.Value;
        }
    }
}
