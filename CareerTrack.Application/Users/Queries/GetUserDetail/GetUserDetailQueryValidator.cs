using FluentValidation;

namespace CareerTrack.Application.Users.Queries.GetUserDetail
{
    public class GetUserDetailQueryValidator : AbstractValidator<GetUserDetailQuery>
    {
        public GetUserDetailQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
