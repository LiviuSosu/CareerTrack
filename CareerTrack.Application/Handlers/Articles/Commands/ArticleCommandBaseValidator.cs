using FluentValidation;

namespace CareerTrack.Application.Handlers.Articles.Commands
{
    public class ArticleCommandBaseValidator : ValidatorCommandBase<ArticleCommandBase>
    {
        public ArticleCommandBaseValidator()
        {
            RuleFor(article => article.Title).NotEmpty();
            //add other rules here
        }
    }
}
