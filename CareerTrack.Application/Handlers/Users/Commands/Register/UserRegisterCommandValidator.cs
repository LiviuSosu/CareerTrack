using FluentValidation;
using System.Text.RegularExpressions;

namespace CareerTrack.Application.Handlers.Users.Commands.Register
{
    public class UserRegisterCommandValidator : BaseValidator<UserRegisterCommand>
    {
        public UserRegisterCommandValidator()
        {
            RuleFor(registerRequest => registerRequest.Username).NotNull();
            RuleFor(registerRequest => registerRequest.Email).EmailAddress();
            Regex r = new Regex("^[a-zA-Z0-9]*$");
            RuleFor(registerRequest => registerRequest.Password).Must(str => r.IsMatch(str));
            RuleFor(registerRequest => registerRequest.RoleId).NotNull();
        }
    }
}
