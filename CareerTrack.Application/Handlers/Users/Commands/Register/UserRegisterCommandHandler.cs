using CareerTrack.Application.Exceptions;
using CareerTrack.Application.Handlers.Articles;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
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
            if (await request.UserManager.FindByNameAsync(request.Username) == null)
            {
                throw new NotFoundException(request.Username, request);
            }
            if(await request.UserManager.FindByEmailAsync(request.Email) != null)
            {
                throw new NotFoundException(request.Email, request);
            }

            var standardUser = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Username,
            };

            await request.UserManager.CreateAsync(standardUser, request.Password);
            _repoWrapper.User.Create(standardUser);

            var identityStandaerdUserRole = new IdentityUserRole<string>
            {
                RoleId = request.RoleId,
                UserId = standardUser.Id
            };

            return Unit.Value;
        }
    }
}
