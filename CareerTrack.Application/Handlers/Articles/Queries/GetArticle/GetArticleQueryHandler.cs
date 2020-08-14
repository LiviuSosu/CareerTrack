using CareerTrack.Persistance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Handlers.Articles.Queries.GetArticle
{
    public class GetArticleQueryHandler : BaseHandler<GetArticleQuery, ArticleLookupModel>, IRequestHandler<GetArticleQuery, ArticleLookupModel>
    // : IRequestHandler<GetArticleQuery, ArticleLookupModel>
    {
        //private readonly CareerTrackDbContext _context;
        //private readonly IMapper _mapper;
        //private IRepositoryWrapper _repoWrapper;

        public GetArticleQueryHandler(CareerTrackDbContext context) : base(context)
        {
            //_context = context;
            //_repoWrapper = new RepositoryWrapper(_context);

            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile<ArticleProfile>();
            //});
            //_mapper = config.CreateMapper();
        }

        public new async Task<ArticleLookupModel> Handle(GetArticleQuery request, CancellationToken cancellationToken)
        {
            var article = await _repoWrapper.Article.FindByIdAsync(request.ArticleId);
            
            return _mapper.Map<ArticleLookupModel>(article);
        }
    }
}
