using CareerTrack.Persistance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Handlers.Users.Commands.Logout
{
    public class UserLoginCommandHandler : BaseHandler<UserLogoutCommand, Unit>, IRequestHandler<UserLogoutCommand, Unit>
    {
        CareerTrackDbContext _context;
        public UserLoginCommandHandler(CareerTrackDbContext context) : base(context) {
            _context = context;
        }

        public new async Task<Unit> Handle(UserLogoutCommand request, CancellationToken cancellationToken)
        {


            return Unit.Value;
        }
    }
}
