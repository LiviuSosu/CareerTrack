using CareerTrack.Application.Exceptions;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Articles.Commands.Delete
{
    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, Unit>
    {
        private readonly CareerTrackDbContext _context;

        public DeleteArticleCommandHandler(CareerTrackDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Articles.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Article), request.Id);
            }

            _context.Articles.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
