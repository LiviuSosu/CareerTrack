using FluentValidation;

namespace CareerTrack.Application.Articles.Commands
{
    public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
    {
        public CreateArticleCommandValidator()
        {
            RuleFor(article => article.Name).NotEmpty();
            //add other rules here
        }
    }
}
