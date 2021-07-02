using CareerTrack.Persistance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Handlers.Users.Commands.Logout
{
    public class UserLogoutCommandHandler : BaseHandler<UserLogoutCommand, Unit>, IRequestHandler<UserLogoutCommand, Unit>
    {
        CareerTrackDbContext _context;
        public UserLogoutCommandHandler(CareerTrackDbContext context) : base(context) {
            _context = context;
        }

        public new async Task<Unit> Handle(UserLogoutCommand request, CancellationToken cancellationToken)
        {
            await request.TokenManager.DeactivateCurrentAsync();

            return Unit.Value;
        }
    }
}
