using AutoMapper;
using CareerTrack.Application.Articles.Queries.GetArticles;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using CareerTrack.Persistance.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Articles.Commands.Create
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, Unit>
    {
        private readonly CareerTrackDbContext context;
        private readonly IMapper _mapper;
        private IRepositoryWrapper _repoWrapper;
        public CreateArticleCommandHandler(CareerTrackDbContext _context)
        {
            context = _context;
            _repoWrapper = new RepositoryWrapper(_context);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ArticleProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public async Task<Unit> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {     
            await context.AddAsync(_mapper.Map<Article>(request));
            await context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
