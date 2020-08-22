using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Handlers.Articles.Commands.Create
{
    public class CreateArticleCommandHandler : BaseHandler<CreateArticleCommand, Unit>, IRequestHandler<CreateArticleCommand, Unit>
    {
        public CreateArticleCommandHandler(CareerTrackDbContext context) : base (context)
        {
        }

        public new async Task<Unit> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            _repoWrapper.Article.Create(_mapper.Map<Article>(request));
            await _repoWrapper.SaveAsync();
            return Unit.Value;
        }
    }
}
