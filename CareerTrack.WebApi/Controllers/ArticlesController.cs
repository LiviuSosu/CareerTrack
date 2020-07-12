using System;
using System.Threading.Tasks;
using CareerTrack.Application.Articles.Queries.GetArticles;
using CareerTrack.Application.Paging;
using CareerTrack.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareerTrack.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArticlesController : BaseController
    {
        private readonly IConfiguration _configuration;

        public ArticlesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetArticles")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> GetArticles([FromQuery] PagingModel paginationModel)
        {
            var actionName = ControllerContext.ActionDescriptor.ActionName;
            try
            {
                //_logger.LogInformation(actionName, JsonConvert.SerializeObject(paginationModel));
                return Ok(await Mediator.Send(new GetArticlesListQuery(paginationModel)));
            }
            catch (Exception exception)
            {
                //_logger.LogException(exception, actionName, JsonConvert.SerializeObject(paginationModel));
                return StatusCode(500, _configuration.DisplayUserErrorMessage);
            }
        }
    }
}
