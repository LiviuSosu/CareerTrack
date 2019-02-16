using AutoMapper;
using CareerTrack.Application.Paging;
using CareerTrack.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Users.Queries.GetUsersList
{
    public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, UsersListViewModel>
    {
        private readonly CareerTrackDbContext _context;
        private readonly IMapper _mapper;

        public GetUsersListQueryHandler(CareerTrackDbContext context)
        {
            _context = context;
            //_mapper = mapper;
        }

        public async Task<UsersListViewModel> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.PagingModel.QueryFilter))
            {
                request.PagingModel.QueryFilter = string.Empty;
            }

            var viewModel = new UsersListViewModel
            {
                Users = await _context.Users.Select(user =>
                    new UserLookupModel
                    {
                        Id = user.UserId,
                        //UserName = user.UserName
                    }).Where(x => x.UserName.ToLower().Contains(request.PagingModel.QueryFilter.ToLower()))
                    .Skip((request.PagingModel.PageNumber - 1) * request.PagingModel.PageSize).Take(request.PagingModel.PageSize)
                    .ToListAsync(cancellationToken)
            };

            switch (request.PagingModel.Field)
            {
                case "Username":
                    if (request.PagingModel.Order == Order.asc)
                    {
                        viewModel.Users = viewModel.Users.OrderBy(user => user.UserName).ToList();
                    }
                    else
                    {
                        viewModel.Users = viewModel.Users.OrderByDescending(user => user.UserName).ToList();
                    }
                    break;
                default:
                    break;
            }

            return viewModel;
        }
    }
}
