using CareerTrack.Application.Paging;
using CareerTrack.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Handlers.Articles.Queries.GetArticles
{
    public class GetArticlesListQueryHandler : BaseHandler<GetArticlesListQuery, ArticlesListViewModel>, IRequestHandler<GetArticlesListQuery, ArticlesListViewModel>
    {
        public GetArticlesListQueryHandler(CareerTrackDbContext context) : base(context)
        {
        }

        public new async Task<ArticlesListViewModel> Handle(GetArticlesListQuery request, CancellationToken cancellationToken)
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
