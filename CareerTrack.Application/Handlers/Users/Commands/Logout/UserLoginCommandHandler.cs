using CareerTrack.Persistance;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Handlers.Users.Commands.Logout
{
    public class UserLoginCommandHandler : BaseHandler<UserLogoutCommand, Unit>, IRequestHandler<UserLogoutCommand, Unit>
    {
        CareerTrackDbContext _context;
        public UserLoginCommandHandler(CareerTrackDbContext context) : base(context) {
            _context = context;
        }

        public new async Task<Unit> Handle(UserLogoutCommand request, CancellationToken cancellationToken)
        {


            return Unit.Value;
        }
    }
}
