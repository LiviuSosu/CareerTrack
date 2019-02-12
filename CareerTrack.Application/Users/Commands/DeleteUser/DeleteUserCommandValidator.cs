using FluentValidation;

namespace CareerTrack.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator: AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
