using AutoMapper;
using CareerTrack.Application.Handlers.Articles.Queries.GetArticles;
using CareerTrack.Persistance;
using CareerTrack.Persistance.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Handlers
{
    public class BaseHandler<TRequest, Unit> : IRequestHandler<TRequest, Unit> where TRequest : IRequest<Unit>
    {
        protected readonly IMapper _mapper;
        protected IRepositoryWrapper _repoWrapper;
        private readonly IRequestHandler<TRequest, Unit> _inner;
        public BaseHandler(CareerTrackDbContext context)
        {
            _repoWrapper = new RepositoryWrapper(context);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ArticleProfile>();
            });
            _mapper = config.CreateMapper();
        }

        public Task<Unit> Handle(TRequest message, CancellationToken cancellationToken)
        {
            return _inner.Handle(message, cancellationToken);
        }
    }
}
