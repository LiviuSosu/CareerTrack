using CareerTrack.Application.Paging;
using CareerTrack.Application.Users.Commands.CreateUser;
using CareerTrack.Application.Users.Commands.DeleteUser;
using CareerTrack.Application.Users.Commands.UpdateCustomer;
using CareerTrack.Application.Users.Queries.GetUserDetail;
using CareerTrack.Application.Users.Queries.GetUsersList;
using CareerTrack.Domain.Entities;
using CareerTrack.WebApi.Login;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CareerTrack.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly UserManager<User> userManager;
        private IServiceProvider Provider { get; set; }
        public UsersController(UserManager<User> userManager, IServiceProvider provider)
        {
            this.userManager = userManager;
            Provider = provider;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]CreateUserCommand command)
        {
            command.UserManager = userManager;
            command.ServiceProvider = Provider;
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            var roles = await userManager.GetRolesAsync(user);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("role",roles[0])
                };

                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureKey"));

                var token = new JwtSecurityToken(
                    issuer: "http://oec.com",
                    audience: "http://oec.com",
                    expires: DateTime.UtcNow.AddHours(1),
                    claims: claims,
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return Unauthorized();
        }

        // GET api/customers
        [HttpGet]
        public async Task<ActionResult<UsersListViewModel>> GetAll([FromQuery]PagingModel paginationModel)
        {
            return Ok(await Mediator.Send(new GetUsersListQuery(paginationModel)));
        }

        // GET api/customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailModel>> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetUserDetailQuery { Id = id }));
        }

        // POST api/customers
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Create([FromBody]CreateUserCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // PUT api/customers/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Update(Guid id, [FromBody]UpdateUserCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE api/customers/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteUserCommand { Id = id });

            return NoContent();
        }
    }
}
