using CareerTrack.Application.Exceptions;
using CareerTrack.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var user = await _repoWrapper.User.FindByCondition(u => u.UserName == request.Username).SingleOrDefaultAsync();
            if (user == null)
            {
                throw new NotFoundException(request.Username, user);
            }

            await request.UserManager.DeleteAsync(user);

            return Unit.Value;
        }
    }
}
