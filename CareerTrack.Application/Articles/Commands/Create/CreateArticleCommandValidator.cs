using FluentValidation;

namespace CareerTrack.Application.Articles.Commands.Create
{
    public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
    {
        public CreateArticleCommandValidator()
        {
            RuleFor(article => article.Title).NotEmpty();
            //add other rules here
        }
    }
}
