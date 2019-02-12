using CareerTrack.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace CareerTrack.Application.Users.Queries.GetUserDetail
{
    public class UserDetailModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }

        public static Expression<Func<User, UserDetailModel>> Projection
        {
            get
            {
                return user => new UserDetailModel
                {
                    Id = user.Id,
                    UserName = user.UserName
                };
            }
        }

        public static UserDetailModel Create(User user)
        {
            return Projection.Compile().Invoke(user);
        }
    }
}
