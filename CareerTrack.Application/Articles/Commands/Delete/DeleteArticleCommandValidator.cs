using FluentValidation;

namespace CareerTrack.Application.Articles.Commands.Delete
{
    public class DeleteArticleCommandValidator : AbstractValidator<DeleteArticleCommand>
    {
        public DeleteArticleCommandValidator()
        {
            //RuleFor(article => article.Id).NotEmpty();
        }
    }
}
