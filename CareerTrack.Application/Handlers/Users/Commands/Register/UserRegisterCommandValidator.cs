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
            RuleFor(registerRequest => registerRequest.Password).Must(str=> ValidatePassword(str));
            RuleFor(registerRequest => registerRequest.RoleId).NotNull();
        }

        bool ValidatePassword(string password)
        {
            var letterPattern = new Regex("[a-zA-Z]+");
            var numberPattern = new Regex("\\d+");
            var alfanumericCharacterPattern = new Regex("\\W+");

            if(letterPattern.IsMatch(password) && numberPattern.IsMatch(password) &&
                alfanumericCharacterPattern.IsMatch(password) && password.Length>=8)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
