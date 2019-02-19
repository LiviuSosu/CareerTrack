using CareerTrack.Application.Articles;
using CareerTrack.Application.Articles.Commands.Delete;
using CareerTrack.Application.Articles.Commands.Update;
using CareerTrack.Application.Articles.Queries.GetArticleDetail;
using CareerTrack.Application.Articles.Queries.GetArticles;
using CareerTrack.Application.Paging;
using CareerTrack.Common;
using CareerTrack.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CareerTrack.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public class ArticlesController : BaseController
    {
        private readonly UserManager<User> userManager;
        private IServiceProvider Provider { get; set; }
        private readonly ILogger _logger;
        public ArticlesController(UserManager<User> userManager, IServiceProvider provider, ILogger logger)
        {
            this.userManager = userManager;
            Provider = provider;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> CreateArticle([FromBody]CreateArticleCommand command, [FromHeader]string Authorization)
        {
            var actionName = ControllerContext.ActionDescriptor.ActionName;
            command.ServiceProvider = Provider;
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> UpdateArticle(Guid id, [FromBody]UpdateArticleCommand command, [FromHeader]string Authorization)
        {
            var actionName = ControllerContext.ActionDescriptor.ActionName;
            command.ServiceProvider = Provider;
            command.Id = id;
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> DeleteArticle(Guid id, [FromBody]DeleteArticleCommand command, [FromHeader]string Authorization)
        {
            var actionName = ControllerContext.ActionDescriptor.ActionName;
            await Mediator.Send(new DeleteArticleCommand { Id = id });
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle(Guid id, [FromHeader]string Authorization)
        {
            var actionName = ControllerContext.ActionDescriptor.ActionName;
            return Ok(await Mediator.Send(new GetArticleDetailQuery() { Id = id }));
        }

        [HttpGet]
        public async Task<IActionResult> GetArticles([FromQuery]PagingModel paginationModel, [FromHeader]string Authorization)
        {
            var actionName = ControllerContext.ActionDescriptor.ActionName;
            try
            {
                _logger.LogInformation(actionName, JsonConvert.SerializeObject(paginationModel), Authorization);
                return Ok(await Mediator.Send(new GetArticlesListQuery(paginationModel)));
            }
            catch (Exception exception)
            {
                _logger.LogException(exception, actionName, JsonConvert.SerializeObject(paginationModel), Authorization);
                return StatusCode(500, Configuration.DisplayUserErrorMessage);
            }
        }
    }
}