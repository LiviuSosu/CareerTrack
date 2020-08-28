using FluentValidation;

namespace CareerTrack.Application.Handlers.Articles.Commands.Create
{
    public class CreateArticleCommandValidator : BaseValidator<CreateArticleCommand> //: AbstractValidator<CreateArticleCommand>
    {
        public CreateArticleCommandValidator()
        {
            RuleFor(article => article.Title).NotEmpty();
            //add other rules here
        }
    }
}
