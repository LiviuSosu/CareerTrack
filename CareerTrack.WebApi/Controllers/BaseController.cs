using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CareerTrack.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BaseController : Controller
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected const int badRequestErrorCode = 400;
        protected const int notFoundErrorCode = 404;
        protected const int internalServerErrorCode = 500;
    }
}
