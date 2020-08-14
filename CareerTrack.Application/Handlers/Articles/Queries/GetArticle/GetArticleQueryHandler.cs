using AutoMapper;
using CareerTrack.Application.Handlers.Articles.Queries.GetArticles;
using CareerTrack.Persistance;
using CareerTrack.Persistance.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Handlers.Articles.Queries.GetArticle
{
    public class GetArticleQueryHandler : IRequestHandler<GetArticleQuery, ArticleLookupModel>
    {
        private readonly CareerTrackDbContext _context;
        private readonly IMapper _mapper;
        private IRepositoryWrapper _repoWrapper;

        public GetArticleQueryHandler(CareerTrackDbContext context)
        {
            _context = context;
            _repoWrapper = new RepositoryWrapper(_context);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ArticleProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public async Task<ArticleLookupModel> Handle(GetArticleQuery request, CancellationToken cancellationToken)
        {
            var article = await _repoWrapper.Article.FindByIdAsync(request.ArticleId);
            
            return _mapper.Map<ArticleLookupModel>(article);
        }
    }
}
