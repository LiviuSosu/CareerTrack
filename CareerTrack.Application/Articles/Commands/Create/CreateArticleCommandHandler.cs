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
    public class CreateArticleCommandHandler : BaseArticleCommandHandler<CreateArticleCommand, Unit>, IRequestHandler<CreateArticleCommand, Unit>
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
