using CareerTrack.Application.Handlers.Articles;
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
            await Task.Delay(200);
            return Unit.Value;
            //_repoWrapper.Article.Create(_mapper.Map<Article>(request));
            //await _repoWrapper.SaveAsync();
            //return Unit.Value;
        }
    }
}
