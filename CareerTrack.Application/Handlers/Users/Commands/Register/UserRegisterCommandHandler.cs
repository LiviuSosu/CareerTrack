using CareerTrack.Application.Exceptions;
using CareerTrack.Application.Handlers.Articles;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
            var x= await request.UserManager.CreateAsync(standardUser, request.Password);

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
