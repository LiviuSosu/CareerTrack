using CareerTrack.Application.Exceptions;
using CareerTrack.Application.Handlers.Users.Commands.DeletePermanenty;
using CareerTrack.Application.Handlers.Users.Commands.Login;
using CareerTrack.Application.Handlers.Users.Commands.Register;
using CareerTrack.Common;
using CareerTrack.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public UsersController(
            UserManager<User> userManager,
            IConfiguration configuration,
            ILogger logger)
        {
            this.userManager = userManager;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginCommand userLoginCommand)
        {
            var actionName = ControllerContext.ActionDescriptor.ActionName;
          
            try
            {
                userLoginCommand.UserManager = userManager;
                userLoginCommand.JWTConfiguration = _configuration.JWTConfiguration;
                return Ok(await Mediator.Send(userLoginCommand));
            }
            catch (NotFoundException)
            {
                return Unauthorized();
            }
            catch (LoginFailedException)
            {
                return Unauthorized();
            }
            catch (Exception exception)
            {
                _logger.LogException(exception, actionName, JsonConvert.SerializeObject(userLoginCommand), string.Empty);
                return StatusCode(500, _configuration.DisplayGenericUserErrorMessage);
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterCommand userRegisterCommand)
        {
            var actionName = ControllerContext.ActionDescriptor.ActionName;

            try
            {
                userRegisterCommand.UserManager = userManager;
                return Ok(await Mediator.Send(userRegisterCommand));
            }
            catch (Exception exception)
            {
                _logger.LogException(exception, actionName, JsonConvert.SerializeObject(userRegisterCommand), string.Empty);
                return StatusCode(500, _configuration.DisplayGenericUserErrorMessage);
            }
        }

        [HttpDelete]
        [Authorize(Policy = "IsAdmin")]
        [Route("DeleteUserPermanently")]
        public async Task<IActionResult> DeleteUserPermanently([FromQuery]  Guid userId)
        {
            var actionName = ControllerContext.ActionDescriptor.ActionName;

            var deleteUserDeleteCommand = new DeleteUserPermanentyCommand();
            deleteUserDeleteCommand.UserId = userId;
            deleteUserDeleteCommand.UserManager = userManager;
        
            try
            {          
                return Ok(await Mediator.Send(deleteUserDeleteCommand));
                //https://docs.microsoft.com/en-us/aspnet/core/security/authentication/accconfirm?view=aspnetcore-3.1&tabs=visual-studio
            }
            catch(NotFoundException exception)
            {
                return StatusCode(404, exception);
            }
            catch (Exception exception)
            {
                _logger.LogException(exception, actionName, JsonConvert.SerializeObject(deleteUserDeleteCommand), string.Empty);
                return StatusCode(500, _configuration.DisplayGenericUserErrorMessage);
            }
        }
    }
}
