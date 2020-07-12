using CareerTrack.Application.Paging;
using CareerTrack.Persistance;
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

        public GetArticlesListQueryHandler(CareerTrackDbContext context)
        {
            _context = context;
        }

        public async Task<ArticlesListViewModel> Handle(GetArticlesListQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.PagingModel.QueryFilter))
            {
                request.PagingModel.QueryFilter = string.Empty;
            }

            var viewModel = new ArticlesListViewModel
            {
                Articles = await _context.Articles.Select(article =>
                    new ArticleLookupModel
                    {
                        Id = article.Id,
                        Title = article.Title,
                        Link = article.Link
                    }).Where(x => x.Title.ToLower().Contains(request.PagingModel.QueryFilter.ToLower()))
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
