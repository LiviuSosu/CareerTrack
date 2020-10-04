using CareerTrack.Application.Exceptions;
using CareerTrack.Persistance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Handlers.Users.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : BaseHandler<UserChangePasswordCommand, Unit>, IRequestHandler<UserChangePasswordCommand, Unit>
    {
        public ChangePasswordCommandHandler(CareerTrackDbContext context) : base(context)
        {
        }

        public new async Task<Unit> Handle(UserChangePasswordCommand request, CancellationToken cancellationToken)
        {
            if (request.NewPassword == request.ConfirmPassword)
            {
                var user = await request.UserManager.FindByNameAsync(request.Username);
                if (user != null)
                {
                    await request.UserManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
                }
                else
                {
                    throw new NotFoundException(request.Username, request);
                }
            }
            else
            {
                throw new PasswordsAreNotTheSameException();
            }

            return Unit.Value;
        }
    }
}
