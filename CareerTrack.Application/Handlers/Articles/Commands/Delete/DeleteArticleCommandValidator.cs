using FluentValidation;

namespace CareerTrack.Application.Handlers.Articles.Commands.Delete
{
    public class DeleteArticleCommandValidator : BaseValidator<DeleteArticleCommand>
    {
        public DeleteArticleCommandValidator()
        {
            RuleFor(article => article.Id).NotNull();
        }
    }
}
