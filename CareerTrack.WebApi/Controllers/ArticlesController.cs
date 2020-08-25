using System;
using System.Threading.Tasks;
using CareerTrack.Application.Exceptions;
using CareerTrack.Application.Handlers.Articles.Commands.Create;
using CareerTrack.Application.Handlers.Articles.Commands.Delete;
using CareerTrack.Application.Handlers.Articles.Commands.Update;
using CareerTrack.Application.Handlers.Articles.Queries.GetArticle;
using CareerTrack.Application.Handlers.Articles.Queries.GetArticles;
using CareerTrack.Application.Paging;
using CareerTrack.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CareerTrack.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public ArticlesController(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetArticles")]
        [Authorize(Policy = "IsStdUser")]
        public async Task<IActionResult> GetArticles([FromQuery] PagingModel paginationModel)
        {
            var actionName = ControllerContext.ActionDescriptor.ActionName;
            try
            {
                _logger.LogInformation(actionName, JsonConvert.SerializeObject(paginationModel),"");
                return Ok(await Mediator.Send(new GetArticlesListQuery(paginationModel)));
            }
            catch (Exception exception)
            {
                _logger.LogException(exception, actionName, JsonConvert.SerializeObject(paginationModel),"");
                return StatusCode(500, _configuration.DisplayGenericUserErrorMessage);
            }
        }

        [HttpGet]
        [Route("GetArticle")]
        [Authorize(Policy = "IsStdUser")]
        public async Task<IActionResult> GetArticle([FromQuery] Guid Id)
        {
            var actionName = ControllerContext.ActionDescriptor.ActionName;
            try
            {
                _logger.LogInformation(actionName, JsonConvert.SerializeObject(Id), "");
                return Ok(await Mediator.Send(new GetArticleQuery(Id)));
            }
            catch (Exception exception)
            {
                _logger.LogException(exception, actionName, JsonConvert.SerializeObject(Id), "");
                return StatusCode(500, _configuration.DisplayGenericUserErrorMessage);
            }
        }

        [HttpPost]
        [Route("AddArticle")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> CreateArticle([FromBody]CreateArticleCommand command)
        {
            var actionName = ControllerContext.ActionDescriptor.ActionName;
            try
            {
                _logger.LogInformation(actionName, JsonConvert.SerializeObject(command), "");
                return Ok(await Mediator.Send(command));
            }
            catch(ValidationException exception)
            {
                return StatusCode(500, JsonConvert.SerializeObject(exception.Failures));
            }
            catch (Exception exception)
            {
                _logger.LogException(exception, actionName, JsonConvert.SerializeObject(command) + " " + JsonConvert.SerializeObject(command), "");
                return StatusCode(500, _configuration.DisplayGenericUserErrorMessage);
            }
        }

        [HttpPut]
        [Route("UpdateArticle")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> UpdateArticle([FromBody] UpdateArticleCommand command)
        {
            var actionName = ControllerContext.ActionDescriptor.ActionName;
            try
            {
                _logger.LogInformation(actionName, JsonConvert.SerializeObject(command), "");
                return Ok(await Mediator.Send(command));
            }
            catch (ValidationException exception)
            {
                return StatusCode(500, JsonConvert.SerializeObject(exception.Failures));
            }
            catch (Exception exception)
            {
                _logger.LogException(exception, actionName, JsonConvert.SerializeObject(command) + " " + JsonConvert.SerializeObject(command), "");
                return StatusCode(500, _configuration.DisplayGenericUserErrorMessage);
            }
        }

        [HttpDelete]
        [Route("DeleteArticle")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> DeleteArticle([FromBody] DeleteArticleCommand command)
        {
            var actionName = ControllerContext.ActionDescriptor.ActionName;
            try
            {
                _logger.LogInformation(actionName, JsonConvert.SerializeObject(command), "");
                return Ok(await Mediator.Send(command));
            }
            catch (DbUpdateConcurrencyException exception)
            {
                _logger.LogException(exception, actionName, JsonConvert.SerializeObject(command) + " " + JsonConvert.SerializeObject(command), "");
                return StatusCode(400, _configuration.DisplayObjectNotFoundErrorMessage);
            }
            catch (Exception exception)
            {
                _logger.LogException(exception, actionName, JsonConvert.SerializeObject(command) + " " + JsonConvert.SerializeObject(command), "");
                return StatusCode(500, _configuration.DisplayGenericUserErrorMessage);
            }
        }
    }
}
