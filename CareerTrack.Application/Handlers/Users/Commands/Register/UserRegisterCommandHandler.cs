using CareerTrack.Application.Exceptions;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using CareerTrack.Services.SendGrid;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Handlers.Users.Commands.Register
{
    public class UserRegisterCommandHandler : BaseHandler<UserRegisterCommand, Unit>, IRequestHandler<UserRegisterCommand, Unit>
    {
        public UserRegisterCommandHandler(CareerTrackDbContext context) : base(context)
        {
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public new async Task<Unit> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            if (await request.UserManager.FindByNameAsync(request.Username) != null)
            {
                throw new ExistentUserException(request.Username);
            }
            if(await request.UserManager.FindByEmailAsync(request.Email) != null)
            {
                throw new ExistentUserException(request.Email);
            }

            var standardUser = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Username,
            };

            _repoWrapper.User.Create(standardUser);
            await request.UserManager.CreateAsync(standardUser, request.Password);

            var identityStandaerdUserRole = new IdentityUserRole<Guid>
            {
                RoleId = request.RoleId,
                UserId = standardUser.Id
            };

            _repoWrapper.UserRole.Create(identityStandaerdUserRole);

            await _repoWrapper.SaveAsync();

            return Unit.Value;
        }
    }
}
