using FluentValidation;

namespace CareerTrack.Application.Articles.Queries.GetArticleDetail
{
    public class GetArticleDetailQueryValidator : AbstractValidator<GetArticleDetailQuery>
    {
        public GetArticleDetailQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
