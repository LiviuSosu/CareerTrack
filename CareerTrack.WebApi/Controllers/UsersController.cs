using CareerTrack.Application.Exceptions;
using CareerTrack.Application.Handlers.Users.Commands.DeletePermanenty;
using CareerTrack.Application.Handlers.Users.Commands.Login;
using CareerTrack.Application.Handlers.Users.Commands.Register;
using CareerTrack.Common;
using CareerTrack.Domain.Entities;
using CareerTrack.Services.SendGrid;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
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
        private readonly IEmailSender _emailSender;

        public UsersController(
            UserManager<User> userManager,
            IConfiguration configuration,
            ILogger logger,
            IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            this.userManager = userManager;
            _configuration = configuration;
            _logger = logger;
            Options = optionsAccessor.Value;
            _emailSender = new EmailSender(Options.SendGridApiKey);
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

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
                await Mediator.Send(userRegisterCommand);

                var user = await userManager.FindByNameAsync("LiviuS");
                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

                string confirmationLink = Url.Action("ConfirmAccount",
                              "Users", new
                              {
                                  userid = user.Id,
                                  token = code
                              },
                               protocol: HttpContext.Request.Scheme);

                await _emailSender.SendConfirmationEmail(new UserRegistrationEmailDTO
                {
                    Email = userRegisterCommand.Email,
                    Username = userRegisterCommand.Username,
                    EmailServiceConfiguration = _configuration.EmailServiceConfiguration,
                    ConfirmationToken = confirmationLink
                });

                return Ok();
            }
            catch(ExistentUserException)
            {
                return StatusCode(500, _configuration.DisplayExistentUserExceptionMessage);
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

        [HttpGet]
        [Route("ConfirmAccount")]
        public async Task<IActionResult> ConfirmAccount([FromQuery] string token, [FromQuery] string userid)
        {
            var user = await userManager.FindByIdAsync(userid);
            var result = await userManager.ConfirmEmailAsync(user, token);
            if(result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, _configuration.DisplayExistentUserExceptionMessage);
            }
        }
    }
}
