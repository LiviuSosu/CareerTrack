using CareerTrack.Application.Exceptions;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly CareerTrackDbContext _context;

        public DeleteUserCommandHandler(CareerTrackDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Users
                .FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            //var hasOrders = _context.Orders.Any(o => o.CustomerId == entity.CustomerId);
            //if (hasOrders)
            //{
            //    throw new DeleteFailureException(nameof(Customer), request.Id, "There are existing orders associated with this customer.");
            //}

            _context.Users.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
