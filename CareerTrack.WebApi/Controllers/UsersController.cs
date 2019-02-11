using CareerTrack.Application.Users.Queries.GetUserDetail;
using CareerTrack.Application.Users.Queries.GetUsersList;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerTrack.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        // GET api/customers
        [HttpGet]
        public async Task<ActionResult<UsersListViewModel>> GetAll()
        {
            return Ok(await Mediator.Send(new GetUsersListQuery()));
        }

        // GET api/customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailModel>> Get(string id)
        {
            return Ok(await Mediator.Send(new GetUserDetailQuery { Id = id }));
        }
    }
}
