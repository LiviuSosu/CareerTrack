using CareerTrack.Application.Handlers.Users.Commands.Login;
using CareerTrack.Common;
using CareerTrack.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
        //private readonly RoleManager<IdentityRole> roleManager;
        private IServiceProvider Provider { get; set; }
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public UsersController(/*RoleManager<IdentityRole> roleManager,*/
            UserManager<User> userManager
            ,IServiceProvider provider,
            IConfiguration configuration,
            ILogger logger)
        {
            //this.roleManager = roleManager;
            this.userManager = userManager;
            Provider = provider;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Test([FromBody] UserLoginCommand userLoginCommand)
        {
            var actionName = ControllerContext.ActionDescriptor.ActionName;
          
            try
            {
                userLoginCommand.userManager = userManager;
                return Ok(await Mediator.Send(userLoginCommand));
                //var user = await userManager.FindByNameAsync(loginModel.Username);
                //if (user == null)
                //{
                //    return Unauthorized();
                //}

                //var roles = await userManager.GetRolesAsync(user);

                //if (user != null && await userManager.CheckPasswordAsync(user, loginModel.Password))
                //{
                //    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.JwtSecretKey));

                //    var token = new JwtSecurityToken(
                //           issuer: _configuration.JwtIssuer,
                //           audience: _configuration.JwtAudience,
                //           expires: DateTime.UtcNow.AddHours(Convert.ToInt16(_configuration.JwtLifeTime)),
                //           claims: await GetRolesAsClaim(user),
                //           signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                //           );

                //    return Ok(new
                //    {
                //        token = new JwtSecurityTokenHandler().WriteToken(token),
                //        expiration = token.ValidTo
                //    });
                //}

                //return Unauthorized();
            }
            catch (Exception exception)
            {
                _logger.LogException(exception, actionName, JsonConvert.SerializeObject(userLoginCommand), string.Empty);
                return StatusCode(500, _configuration.DisplayGenericUserErrorMessage);
            }
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
            stringRoles.Remove(stringRoles.Length - 1, 1);

            result.Add(new Claim("roles", stringRoles.ToString()));
            return result;
        }
    }
}
