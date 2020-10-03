using CareerTrack.Common;
using MediatR;

namespace CareerTrack.Application.Handlers.Users.Commands.Login
{
    public class UserLoginCommand : UserCommandRequestBase, IRequest<LoginResponseDTO>
    {
        public string Password { get; set; }

        public JWTConfiguration JWTConfiguration  { get; set; }
    }
}
