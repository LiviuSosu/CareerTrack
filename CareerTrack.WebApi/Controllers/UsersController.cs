using CareerTrack.Application.Exceptions;
using CareerTrack.Application.Handlers.Users.Commands.ChangePassword;
using CareerTrack.Application.Handlers.Users.Commands.DeletePermanenty;
using CareerTrack.Application.Handlers.Users.Commands.Login;
using CareerTrack.Application.Handlers.Users.Commands.Register;
using CareerTrack.Application.Handlers.Users.Commands.ResetPassword;
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
                return StatusCode(notFoundErrorCode);
            }
            catch (LoginFailedException)
            {
                return Unauthorized();
            }
            catch (NoRolesAssignedException)
            {
                return StatusCode(internalServerErrorCode, _configuration.NoRolesAssignedExceptionMessage);
            }
            catch (Exception exception)
            {
                _logger.LogException(exception, actionName, JsonConvert.SerializeObject(userLoginCommand), string.Empty);
                return StatusCode(internalServerErrorCode, _configuration.DisplayGenericUserErrorMessage);
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

                var user = await userManager.FindByEmailAsync(userRegisterCommand.Email);
                if (user!=null)
                {
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
                else
                {
                    return StatusCode(notFoundErrorCode, _configuration.DisplayObjectNotFoundErrorMessage);
                }
         
            }
            catch (ExistentUserException)
            {
                return StatusCode(badRequestErrorCode, _configuration.DisplayExistentUserExceptionMessage);
            }
            catch (Exception exception)
            {
                _logger.LogException(exception, actionName, JsonConvert.SerializeObject(userRegisterCommand), string.Empty);
                return StatusCode(internalServerErrorCode, _configuration.DisplayGenericUserErrorMessage);
            }
        }

        [HttpDelete]
        [Authorize(Policy = "IsAdmin")]
        [Route("DeleteUserPermanently")]
        public async Task<IActionResult> DeleteUserPermanently([FromQuery] string Username, [FromHeader] string Authorization)
        {
            var actionName = ControllerContext.ActionDescriptor.ActionName;

            var deleteUserDeleteCommand = new DeleteUserPermanentyCommand();
            deleteUserDeleteCommand.Username = Username;
            deleteUserDeleteCommand.UserManager = userManager;

            try
            {
                return Ok(await Mediator.Send(deleteUserDeleteCommand));
            }
            catch (NotFoundException exception)
            {
                return StatusCode(notFoundErrorCode, exception);
            }
            catch (ValidationException exception)
            {
                return StatusCode(badRequestErrorCode, JsonConvert.SerializeObject(exception.Failures));
            }
            catch (Exception exception)
            {
                _logger.LogException(exception, actionName, JsonConvert.SerializeObject(deleteUserDeleteCommand), Authorization);
                return StatusCode(internalServerErrorCode, _configuration.DisplayGenericUserErrorMessage);
            }
        }

        [HttpGet]
        [Route("ConfirmAccount")]
        public async Task<IActionResult> ConfirmAccount([FromQuery] string token, [FromQuery] string userid)
        {
            var user = await userManager.FindByIdAsync(userid);
            if(user!=null)
            {
                var result = await userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(internalServerErrorCode, _configuration.DisplayExistentUserExceptionMessage);
                }
            }
            else
            {
                return StatusCode(notFoundErrorCode, _configuration.DisplayObjectNotFoundErrorMessage);
            }
        }

        [HttpPut]
        [Authorize]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand userChangePasswordCommand, [FromHeader] string Authorization)
        {
            var actionName = ControllerContext.ActionDescriptor.ActionName;
            try
            {
                userChangePasswordCommand.UserManager = userManager;
                return Ok(await Mediator.Send(userChangePasswordCommand));
            }
            catch (NotFoundException)
            {
                return StatusCode(notFoundErrorCode, _configuration.DisplayObjectNotFoundErrorMessage);
            }
            catch (PasswordsAreNotTheSameException)
            {
                return StatusCode(badRequestErrorCode, _configuration.DisplayPasswordsAreNotTheSameExceptionMessage);
            }
            catch (Exception exception)
            {
                _logger.LogException(exception, actionName, JsonConvert.SerializeObject(userChangePasswordCommand), Authorization);
                return StatusCode(internalServerErrorCode, _configuration.DisplayGenericUserErrorMessage);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromQuery] string userName, [FromHeader] string Authorization)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(userName);
                if(user!=null)
                {
                    var code = await userManager.GeneratePasswordResetTokenAsync(user);
                    return Ok(code);
                }     
                else
                {
                    return StatusCode(notFoundErrorCode, _configuration.DisplayObjectNotFoundErrorMessage);
                }
            }
            catch 
            {
                return StatusCode(internalServerErrorCode, _configuration.DisplayGenericUserErrorMessage);
            }
        }

        [HttpPut]
        [Authorize]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] UserResetPasswordCommand userResetPasswordCommand, [FromHeader] string Authorization)
        {
            var actionName = ControllerContext.ActionDescriptor.ActionName;

            try
            {
                userResetPasswordCommand.UserManager = userManager;
                return Ok(await Mediator.Send(userResetPasswordCommand));
            }
            catch (PasswordsAreNotTheSameException)
            {
                return StatusCode(badRequestErrorCode, _configuration.DisplayPasswordsAreNotTheSameExceptionMessage);
            }
            catch (NotFoundException)
            {
                return StatusCode(notFoundErrorCode, _configuration.DisplayObjectNotFoundErrorMessage);
            }
            catch(Exception exception)
            {
                _logger.LogException(exception, actionName, JsonConvert.SerializeObject(userResetPasswordCommand), Authorization);
                return StatusCode(internalServerErrorCode, _configuration.DisplayGenericUserErrorMessage);
            }
        }

        public async Task<IActionResult> ChangePassword(UserResetPasswordCommand userResetPasswordCommand, [FromHeader] string Authorization)
        {
           // userManager.ChangePasswordAsync("");
            return null;
        }
    }
}
