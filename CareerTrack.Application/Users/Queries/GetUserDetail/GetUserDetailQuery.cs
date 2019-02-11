using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerTrack.Application.Users.Queries.GetUserDetail
{
    public class GetUserDetailQuery : IRequest<UserDetailModel>
    {
        public string Id { get; set; }
    }
}
