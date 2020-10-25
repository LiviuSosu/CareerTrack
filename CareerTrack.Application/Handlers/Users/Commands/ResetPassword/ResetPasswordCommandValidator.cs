using FluentValidation;

namespace CareerTrack.Application.Handlers.Users.Commands.ResetPassword
{
    public class ResetPasswordCommandValidator : UserCommandBaseValidator<UserResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(registerRequest => registerRequest.Token).NotNull();
            RuleFor(registerRequest => registerRequest.NewPassword).Must(str => ValidatePassword(str));
            RuleFor(registerRequest => registerRequest.ConfirmPassword).Must(str => ValidatePassword(str));
        }       
    }
}
