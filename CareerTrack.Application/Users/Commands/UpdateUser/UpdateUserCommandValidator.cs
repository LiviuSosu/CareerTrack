using FluentValidation;

namespace CareerTrack.Application.Users.Commands.UpdateCustomer
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
