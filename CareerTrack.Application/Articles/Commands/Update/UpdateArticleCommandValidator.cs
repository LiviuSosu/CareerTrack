using FluentValidation;

namespace CareerTrack.Application.Articles.Commands.Update
{
    public class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
    {
        public UpdateArticleCommandValidator()
        {
            RuleFor(article => article.Name).NotEmpty();
        }
    }
}
