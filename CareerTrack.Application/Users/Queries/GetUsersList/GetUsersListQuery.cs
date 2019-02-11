using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerTrack.Application.Users.Queries.GetUsersList
{
    public class GetUsersListQuery : IRequest<UsersListViewModel>
    {
    }
}
