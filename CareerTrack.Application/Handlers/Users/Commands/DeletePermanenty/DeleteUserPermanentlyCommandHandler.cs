using CareerTrack.Application.Exceptions;
using CareerTrack.Application.Handlers.Articles;
using CareerTrack.Persistance;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Handlers.Users.Commands.DeletePermanenty
{
    public class DeleteUserPermanentlyCommandHandler : BaseHandler<DeleteUserPermanentyCommand, Unit>, IRequestHandler<DeleteUserPermanentyCommand, Unit>
    {
        public DeleteUserPermanentlyCommandHandler(IOptions<AuthMessageSenderOptions> optionsAccessor
            , CareerTrackDbContext context) : base(context)
        {
            Options = optionsAccessor.Value;
        }
        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager
        public new async Task<Unit> Handle(DeleteUserPermanentyCommand request, CancellationToken cancellationToken)
        {
            //var user  = await _repoWrapper.User.FindByIdAsync(request.UserId);
            //await request.UserManager.DeleteAsync(user);
            var x = Options.SendGridUser;
            await Task.Delay(100);
            throw new NotFoundException(x,x);
            
            return Unit.Value;
        }
    }
}
