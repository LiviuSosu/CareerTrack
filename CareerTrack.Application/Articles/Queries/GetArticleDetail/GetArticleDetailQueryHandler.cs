using CareerTrack.Application.Exceptions;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Articles.Queries.GetArticleDetail
{
    public class GetArticleDetailQueryHandler : IRequestHandler<GetArticleDetailQuery, ArticleDetailModel>
    {
        private readonly CareerTrackDbContext _context;

        public GetArticleDetailQueryHandler(CareerTrackDbContext context)
        {
            _context = context;
        }

        public async Task<ArticleDetailModel> Handle(GetArticleDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Articles.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Article), request.Id);
            }

            return new ArticleDetailModel
            {
                Id = entity.Id,
                Link =entity.Link,
                Name = entity.Name
            };
        }
    }
}
