using CareerTrack.Common;
using MediatR;

namespace CareerTrack.Application.Handlers.Users.Commands.Login
{
    public class UserLoginCommand : UserCommandRequestBase, IRequest<LoginResponseDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public JWTConfiguration JWTConfiguration  { get; set; }
    }
}
