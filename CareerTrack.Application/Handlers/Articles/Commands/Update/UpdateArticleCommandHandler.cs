using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Handlers.Articles.Commands.Update
{
    public class UpdateArticleCommandHandler : BaseHandler<UpdateArticleCommand, Unit>, IRequestHandler<UpdateArticleCommand, Unit>
    {
        public UpdateArticleCommandHandler(CareerTrackDbContext context) : base(context)
        {
        }

        public new async Task<Unit> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            _repoWrapper.Article.Update(_mapper.Map<Article>(request));
            await _repoWrapper.SaveAsync();
            return Unit.Value;
        }
    }
}
