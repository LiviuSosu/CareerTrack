using AutoMapper;
using CareerTrack.Application.Paging;
using CareerTrack.Persistance;
using CareerTrack.Persistance.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Articles.Queries.GetArticles
{
    public class GetArticlesListQueryHandler : IRequestHandler<GetArticlesListQuery, ArticlesListViewModel>
    {
        private readonly CareerTrackDbContext _context;
        private readonly IMapper _mapper;
        private IRepositoryWrapper _repoWrapper;
        public GetArticlesListQueryHandler(CareerTrackDbContext context)
        {
            _context = context;

            _repoWrapper = new RepositoryWrapper(_context);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ArticleProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public async Task<ArticlesListViewModel> Handle(GetArticlesListQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.PagingModel.QueryFilter))
            {
                request.PagingModel.QueryFilter = string.Empty;
            }

            var viewModel = new ArticlesListViewModel
            {
                Articles = await
                _repoWrapper.Article.FindByCondition(dto => dto.Title.ToLower().Contains(request.PagingModel.QueryFilter.ToLower()))           
                .Select(article =>
                    _mapper.Map<ArticleLookupModel>(article)
                )
               .Skip((request.PagingModel.PageNumber - 1) * request.PagingModel.PageSize).Take(request.PagingModel.PageSize)
               .ToListAsync(cancellationToken)
            };

            switch (request.PagingModel.Field)
            {
                case "Title":
                    if (request.PagingModel.Order == Order.asc)
                    {
                        viewModel.Articles = viewModel.Articles.OrderBy(user => user.Title).ToList();
                    }
                    else
                    {
                        viewModel.Articles = viewModel.Articles.OrderByDescending(user => user.Title).ToList();
                    }
                    break;
                default:
                    break;
            }

            return viewModel;
        }
    }
}
