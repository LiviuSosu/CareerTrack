using CareerTrack.Application.Handlers.Articles;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Handlers.Users.Commands.Login
{
    public class UserLoginCommandHandler : BaseHandler<UserLoginCommand, Unit>, IRequestHandler<UserLoginCommand, Unit>
    {
        public UserLoginCommandHandler(CareerTrackDbContext context) : base(context)
        {

        }

        public new async Task<Unit> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            _repoWrapper.User.FindByCondition(u=>u.Email== request.Username);
            //await _repoWrapper.SaveAsync();
            return Unit.Value;
        }
    }
}
