using CareerTrack.Application.Exceptions;
using CareerTrack.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            if (request.NewPassword==request.ConfirmPassword)
            {
                var user = await _repoWrapper.User.FindByCondition(u => u.UserName == request.Username).FirstOrDefaultAsync();
                if (user != null)
                {
                    await request.UserManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
                    return Unit.Value;
                }
                else
                {
                    throw new NotFoundException(user.UserName, user);
                }        
            }
            else
            {
                throw new PasswordsAreNotTheSameException();
            }       
        }
    }
}
