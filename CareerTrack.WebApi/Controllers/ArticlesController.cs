using System;
using System.Threading.Tasks;
using CareerTrack.Application.Articles.Queries.GetArticle;
using CareerTrack.Application.Articles.Queries.GetArticles;
using CareerTrack.Application.Paging;
using CareerTrack.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
                return StatusCode(500, _configuration.DisplayUserErrorMessage);
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
                return StatusCode(500, _configuration.DisplayUserErrorMessage);
            }
        }
    }
}
