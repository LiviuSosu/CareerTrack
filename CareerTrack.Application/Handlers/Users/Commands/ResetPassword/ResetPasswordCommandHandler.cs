using CareerTrack.Persistance;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Handlers.Users.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : BaseHandler<UserResetPasswordCommand, Unit>, IRequestHandler<UserResetPasswordCommand, Unit>
    {
        public ResetPasswordCommandHandler(CareerTrackDbContext context) : base(context)
        {
        }

        public new async Task<Unit> Handle(UserResetPasswordCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            return Unit.Value;
        }
    }
}
