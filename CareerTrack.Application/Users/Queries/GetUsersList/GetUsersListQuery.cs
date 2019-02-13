using CareerTrack.Application.Pagination;
using MediatR;

namespace CareerTrack.Application.Users.Queries.GetUsersList
{
    public class GetUsersListQuery : IRequest<UsersListViewModel>
    {
        public PagingModel PagingModel { get; set; }

        public GetUsersListQuery(PagingModel pagingModel)
        {
            PagingModel = pagingModel;
        }
    }
}
