using FluentValidation;

namespace CareerTrack.Application.Handlers.Users.Commands.Login
{
    public class UserLoginCommandValidator : BaseValidator<UserLoginCommand>
    {
        public UserLoginCommandValidator()
        {
            RuleFor(loginRequest => loginRequest.Username).EmailAddress();
            RuleFor(loginRequest => loginRequest.Password).NotEmpty();
        }
    }
}
