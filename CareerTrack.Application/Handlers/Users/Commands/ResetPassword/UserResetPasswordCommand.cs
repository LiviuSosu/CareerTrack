using MediatR;

namespace CareerTrack.Application.Handlers.Users.Commands.ResetPassword
{
    public class UserResetPasswordCommand : UserCommandRequestBase, IRequest
    {
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
