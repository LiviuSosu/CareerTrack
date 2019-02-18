using CareerTrack.Application.Articles;
using CareerTrack.Application.Articles.Commands.Delete;
using CareerTrack.Application.Articles.Commands.Update;
using CareerTrack.Application.Articles.Queries.GetArticleDetail;
using CareerTrack.Application.Articles.Queries.GetArticles;
using CareerTrack.Application.Paging;
using CareerTrack.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public ArticlesController(UserManager<User> userManager, IServiceProvider provider)
        {
            this.userManager = userManager;
            Provider = provider;
        }

        [HttpPost]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> CreateArticle([FromBody]CreateArticleCommand command, [FromHeader]string Authorization)
        {
            command.ServiceProvider = Provider;
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> UpdateArticle(Guid id,[FromBody]UpdateArticleCommand command, [FromHeader]string Authorization)
        {
            command.ServiceProvider = Provider;
            command.Id = id;
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> DeleteArticle(Guid id, [FromBody]DeleteArticleCommand command, [FromHeader]string Authorization)
        {
            await Mediator.Send(new DeleteArticleCommand { Id = id});

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle(Guid id, [FromHeader]string Authorization)
        {           
            return Ok(await Mediator.Send(new GetArticleDetailQuery() { Id = id })) ;
        }

        [HttpGet]
        public async Task<IActionResult> GetArticles([FromQuery]PagingModel paginationModel, [FromHeader]string Authorization)
        {
            return Ok(await Mediator.Send(new GetArticlesListQuery(paginationModel)));
        }
    }
}