using MediatR;
using System;

namespace CareerTrack.Application.Users.Queries.GetUserDetail
{
    public class GetUserDetailQuery : IRequest<UserDetailModel>
    {
        public Guid Id { get; set; }
    }
}
