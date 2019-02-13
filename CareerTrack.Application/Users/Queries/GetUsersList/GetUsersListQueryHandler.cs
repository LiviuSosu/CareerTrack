using CareerTrack.Application.Pagination;
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

        public GetUsersListQueryHandler(CareerTrackDbContext context)
        {
            _context = context;
        }

        public async Task<UsersListViewModel> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var viewModel = new UsersListViewModel
            {
                Users = await _context.Users.Select(user =>
                    new UserLookupModel
                    {
                        Id = user.Id,
                        UserName = user.UserName
                    }).Where(x => x.UserName.ToLower().Contains(request.Pagination.QueryFilter.ToLower()))
                    .Skip((request.Pagination.PageNumber - 1) * request.Pagination.PageSize).Take(request.Pagination.PageSize)
                    .ToListAsync(cancellationToken)
            };

            switch (request.Pagination.Field)
            {
                case "Username":
                    if (request.Pagination.Order == Order.asc)
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
