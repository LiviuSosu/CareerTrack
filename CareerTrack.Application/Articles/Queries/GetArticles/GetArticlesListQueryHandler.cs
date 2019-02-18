using AutoMapper;
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
        private readonly IMapper _mapper;

        public GetArticlesListQueryHandler(CareerTrackDbContext context)
        {
            _context = context;
            //_mapper = mapper;
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
                        Name = article.Name,
                        Link = article.Link
                    }).Where(x => x.Name.ToLower().Contains(request.PagingModel.QueryFilter.ToLower()))
                    .Skip((request.PagingModel.PageNumber - 1) * request.PagingModel.PageSize).Take(request.PagingModel.PageSize)
                    .ToListAsync(cancellationToken)
            };

            switch (request.PagingModel.Field)
            {
                case "Name":
                    if (request.PagingModel.Order == Order.asc)
                    {
                        viewModel.Articles = viewModel.Articles.OrderBy(user => user.Name).ToList();
                    }
                    else
                    {
                        viewModel.Articles = viewModel.Articles.OrderByDescending(user => user.Name).ToList();
                    }
                    break;
                default:
                    break;
            }

            return viewModel;
        }
    }
}
