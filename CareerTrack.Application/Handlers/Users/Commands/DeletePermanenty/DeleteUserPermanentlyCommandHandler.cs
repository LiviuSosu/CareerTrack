using CareerTrack.Application.Exceptions;
using CareerTrack.Application.Handlers.Articles;
using CareerTrack.Persistance;
using CareerTrack.Services.SendGrid;
using MediatR;
using Microsoft.Extensions.Options;
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
            if(user == null)
            {
                throw new NotFoundException(user.Email,user);
            }
            await request.UserManager.DeleteAsync(user);
            
            return Unit.Value;
        }
    }
}
