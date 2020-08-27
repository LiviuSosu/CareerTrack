using MediatR;

namespace CareerTrack.Application.Handlers.Users.Commands.Login
{
    public class UserLoginCommand : IRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
