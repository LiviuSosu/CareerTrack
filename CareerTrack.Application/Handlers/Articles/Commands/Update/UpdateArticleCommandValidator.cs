using FluentValidation;

namespace CareerTrack.Application.Handlers.Articles.Commands.Update
{
    public class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
    {
        public UpdateArticleCommandValidator()
        {
            RuleFor(article => article.Title).NotEmpty();
            RuleFor(article => article.Id).NotNull();
        }
    }
}
