using AutoMapper;
using CareerTrack.Application.Articles.Queries.GetArticles;
using CareerTrack.Persistance;
using CareerTrack.Persistance.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Articles
{
    public class BaseArticleCommandHandler<TRequest, Unit>
          : IRequestHandler<TRequest, Unit> 
         where TRequest : IRequest<Unit>
       // where T : class, IRequestHandler<T, Unit>
    {
        protected readonly IMapper _mapper;
        protected IRepositoryWrapper _repoWrapper;
        private readonly IRequestHandler<TRequest, Unit> _inner;
        public BaseArticleCommandHandler(CareerTrackDbContext context)
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

        //public virtual async Task<MediatR.Unit> Handle(T request, CancellationToken cancellationToken)
        //{
        //    return MediatR.Unit.Value;
        //}

        //public virtual async Task<Unit> Handle(T request, CancellationToken cancellationToken)
        //{
       // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/
       //https://docs.microsoft.com/en-us/dotnet/standard/generics/
       //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/generic-methods

                //https://lostechies.com/jimmybogard/2016/10/13/mediatr-pipeline-examples/
        //    return Unit.Value;
        //}
    }
}
