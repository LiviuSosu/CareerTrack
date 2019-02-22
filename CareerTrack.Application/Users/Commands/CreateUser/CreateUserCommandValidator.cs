using FluentValidation;

namespace CareerTrack.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.UserName).NotNull();
            RuleFor(x => x.UserEmail).EmailAddress();
            RuleFor(x => x.Password).NotNull();
        }
    }
}
