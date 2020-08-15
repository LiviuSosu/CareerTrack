using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Handlers.Articles.Commands.Delete
{
    public class DeleteArticleCommandHandler : BaseHandler<DeleteArticleCommand, Unit>, IRequestHandler<DeleteArticleCommand, Unit>
    {
        public DeleteArticleCommandHandler(CareerTrackDbContext context) : base(context)
        {
        }

        public new async Task<Unit> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            _repoWrapper.Article.Delete(_mapper.Map<Article>(request));
            await _repoWrapper.SaveAsync();
            return Unit.Value;
        }
    }
}
