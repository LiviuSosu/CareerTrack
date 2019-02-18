using CareerTrack.Application.Interfaces;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Articles.Commands
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, Unit>
    {
        private readonly CareerTrackDbContext _context;

        public CreateArticleCommandHandler(CareerTrackDbContext context, INotificationService notificationService)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var context = request.ServiceProvider.GetRequiredService<CareerTrackDbContext>();

            var article = new Article
            {
                Link = request.Link,
                Name = request.Name
            };

            await context.AddAsync(article);
            await context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
