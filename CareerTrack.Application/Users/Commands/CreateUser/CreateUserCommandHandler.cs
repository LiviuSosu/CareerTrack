using CareerTrack.Application.Interfaces;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly CareerTrackDbContext _context;

        public CreateUserCommandHandler(CareerTrackDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var context = request.ServiceProvider.GetRequiredService<CareerTrackDbContext>();

            var userManager = request.ServiceProvider.GetRequiredService<UserManager<User>>();

            User user = new User
            {
                UserId = Guid.NewGuid(),
                Email = request.UserEmail,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.UserName,
            };

            await userManager.CreateAsync(user, request.Password);
            return Unit.Value;
        }
    }
}
