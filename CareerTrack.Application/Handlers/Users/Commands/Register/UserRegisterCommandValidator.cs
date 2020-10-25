using FluentValidation;

namespace CareerTrack.Application.Handlers.Users.Commands.Register
{
    public class UserRegisterCommandValidator : UserCommandBaseValidator<UserRegisterCommand>
    {
        public UserRegisterCommandValidator()
        {
            RuleFor(registerRequest => registerRequest.Username).NotNull();
            RuleFor(registerRequest => registerRequest.Email).EmailAddress();
            RuleFor(registerRequest => registerRequest.Password).Must(str => ValidatePassword(str));
        }
    }
}
