using FluentValidation;

namespace CareerTrack.Application.Handlers.Users.Commands
{
    public class UserCommandBaseValidator<T> : BaseValidator<T> where T : UserCommandRequestBase
    {
        public UserCommandBaseValidator()
        {
            RuleFor(request => request.Username).NotEmpty();
        }
    }
}
