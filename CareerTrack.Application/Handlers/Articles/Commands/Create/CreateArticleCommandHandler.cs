using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Handlers.Articles.Commands.Create
{
    public class CreateArticleCommandHandler : BaseHandler<CreateArticleCommand, Unit>, IRequestHandler<CreateArticleCommand, Unit>
    {
        //private readonly IMapper _mapper;
        //private IRepositoryWrapper _repoWrapper;
        public CreateArticleCommandHandler(CareerTrackDbContext context) : base (context)
        {
            //_repoWrapper = new RepositoryWrapper(context);
            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile<ArticleProfile>();
            //});
            //_mapper = config.CreateMapper();
        }

        public new async Task<Unit> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            _repoWrapper.Article.Create(_mapper.Map<Article>(request));
            await _repoWrapper.SaveAsync();
            return Unit.Value;
        }
    }
}
