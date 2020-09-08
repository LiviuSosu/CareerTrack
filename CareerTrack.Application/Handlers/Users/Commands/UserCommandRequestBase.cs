using CareerTrack.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CareerTrack.Application.Handlers.Users.Commands
{
    public class UserCommandRequestBase
    {
        public UserManager<User> UserManager { get; set; }
    }
}
