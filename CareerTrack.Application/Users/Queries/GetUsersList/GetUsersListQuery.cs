using MediatR;

namespace CareerTrack.Application.Users.Queries.GetUsersList
{
    public class GetUsersListQuery : IRequest<UsersListViewModel>
    {
        public Pagination.PaginationModel Pagination { get; set; }
    }
}
