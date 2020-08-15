using FluentValidation;

namespace CareerTrack.Application.Handlers
{
    public class ValidatorCommandBase<T> : AbstractValidator<T>
    {
        public ValidatorCommandBase() { }
    }
}
