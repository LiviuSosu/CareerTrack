using FluentValidation;

namespace CareerTrack.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            //RuleFor(x => x.Id).NotEmpty();
            //add other rules here
        }
    }
}
