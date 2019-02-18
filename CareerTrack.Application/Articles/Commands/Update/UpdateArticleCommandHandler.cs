using CareerTrack.Application.Exceptions;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Articles.Commands.Update
{
    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, Unit>
    {
        private readonly CareerTrackDbContext _context;

        public UpdateArticleCommandHandler(CareerTrackDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Articles.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Article), request.Id);
            }

            entity.Link = request.Link;
            entity.Name = request.Name;

            _context.Articles.Update(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
