using CareerTrack.Common;
using MediatR.Pipeline;
using System.Threading;
using System.Threading.Tasks;

namespace CareerTrack.Application.Infrastructure
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;

        public RequestLogger(ILogger logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            CancellationToken cancelToken = cancellationToken;
            if (cancelToken.IsCancellationRequested)
            {
                cancelToken.ThrowIfCancellationRequested();
            }
            return Task.CompletedTask;
        }
    }
}
