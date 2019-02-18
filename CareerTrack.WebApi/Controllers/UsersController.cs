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
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CareerTrack.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public class UsersController : BaseController
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private IServiceProvider Provider { get; set; }
        public UsersController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager,  IServiceProvider provider)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            Provider = provider;
        }

        [HttpPost]
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
                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Common.Configuration.SecretKey));

                var token = new JwtSecurityToken(
                    issuer: Common.Configuration.Issuer,
                    audience: Common.Configuration.Audience,
                    expires: DateTime.UtcNow.AddHours(Common.Configuration.JwtLifeTime),                    
                    claims: await GetRolesAsClaim(user),
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
        public async Task<IActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteUserCommand { Id = id });

            return NoContent();
        }

        private async Task<List<Claim>> GetClaimsForUser(User user)
        {
            var result = new List<Claim>();
            var roles = await userManager.GetRolesAsync(user);

            result.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName));

            foreach (var role in roleManager.Roles)
            {
                var claims = await roleManager.GetClaimsAsync(new IdentityRole() { Id = role.Id, Name = role.Name });
                foreach (var claim in claims)
                {
                    result.Add(new Claim(claim.Type, claim.Value));
                }
            }

            return result;
        }

        private async Task<List<Claim>> GetRolesAsClaim(User user)
        {
            var result = new List<Claim>();

            var roles = await userManager.GetRolesAsync(user);

            var stringRoles = new StringBuilder();

            foreach (var role in roles)
            {
                stringRoles.Append(role);
                stringRoles.Append(',');
            }
            stringRoles.Remove(stringRoles.Length-1,1);

            result.Add(new Claim("roles", stringRoles.ToString()));
            return result;
        }
    }
}
