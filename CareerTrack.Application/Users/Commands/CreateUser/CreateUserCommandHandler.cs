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
        private readonly INotificationService _notificationService;

        public CreateUserCommandHandler(CareerTrackDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var context = request.ServiceProvider.GetRequiredService<CareerTrackDbContext>();

            var userManager = request.ServiceProvider.GetRequiredService<UserManager<User>>();
       
            //User user = new User
            //{
            //    UserId = Guid.NewGuid(),
            //    Email = "a4@b.com",
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //    UserName = "Casper4",
            //};

            //await userManager.CreateAsync(user, "Password@123");

            return Unit.Value;
        }
    }
}
