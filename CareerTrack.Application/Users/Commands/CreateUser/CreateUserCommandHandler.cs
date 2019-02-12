using CareerTrack.Application.Interfaces;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using MediatR;
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
            var entity = new User
            {
                Id = request.Id,
                UserName = request.UserName
            };

            _context.Users.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
