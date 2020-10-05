using FluentValidation;
using System.Text.RegularExpressions;

namespace CareerTrack.Application.Handlers.Users.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : BaseValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(registerRequest => registerRequest.NewPassword).Must(str => ValidatePassword(str));
        }

        bool ValidatePassword(string password)
        {
            var letterPattern = new Regex("[a-zA-Z]+");
            var numberPattern = new Regex("\\d+");
            var alfanumericCharacterPattern = new Regex("\\W+");

            if (letterPattern.IsMatch(password) && numberPattern.IsMatch(password) &&
                alfanumericCharacterPattern.IsMatch(password) && password.Length >= 8)
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
