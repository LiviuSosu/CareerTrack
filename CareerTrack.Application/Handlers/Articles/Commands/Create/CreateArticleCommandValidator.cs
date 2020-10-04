using FluentValidation;

namespace CareerTrack.Application.Handlers.Articles.Commands.Create
{
    public class CreateArticleCommandValidator : BaseValidator<CreateArticleCommand>
    {
        public CreateArticleCommandValidator()
        {
            RuleFor(article => article.Title).NotEmpty();
        }
    }
}
