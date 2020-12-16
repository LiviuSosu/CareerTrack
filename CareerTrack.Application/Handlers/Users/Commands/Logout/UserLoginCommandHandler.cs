using CareerTrack.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
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

           var userToken =  _repoWrapper.UserToken.FindByCondition(ut=>ut.Value== request.Token).FirstOrDefault() ;


            //if (user == null)
            //{
            //    throw new NotImplementedException();
            //}

            //var refreshToken = user.UserTokens.Single(x => x.Value == request.Token);

            //if (!refreshToken.IsActive)
            //{
            //    throw new NotImplementedException();
            //}
            userToken.Revoked = DateTime.UtcNow;
            _repoWrapper.UserToken.Update(userToken);
            await _repoWrapper.SaveAsync();
            //_context.Update(user);
            //_context.SaveChanges();

            return Unit.Value;
        }
    }
}
