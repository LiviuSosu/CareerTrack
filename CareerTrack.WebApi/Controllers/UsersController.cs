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
        public async Task<IActionResult> DeleteUserPermanently([FromQuery] string Username)
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
            catch (PasswordsAreNotTheSameException)
            {
                return StatusCode(badRequestErrorCode, _configuration.DisplayPasswordsAreNotTheSameExceptionMessage);
            }
            catch (Exception exception)
            {
                _logger.LogException(exception, actionName, JsonConvert.SerializeObject(deleteUserDeleteCommand), string.Empty);
                return StatusCode(internalServerErrorCode, _configuration.DisplayGenericUserErrorMessage);
            }
        }

        [HttpGet]
        [Route("ConfirmAccount")]
        public async Task<IActionResult> ConfirmAccount([FromQuery] string token, [FromQuery] string userid)
        {
            var user = await userManager.FindByIdAsync(userid);
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

        [HttpPut]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand userChangePasswordCommand)
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
                _logger.LogException(exception, actionName, JsonConvert.SerializeObject(userChangePasswordCommand), string.Empty);
                return StatusCode(internalServerErrorCode, _configuration.DisplayGenericUserErrorMessage);
            }
        }

        [HttpPut]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] UserResetPasswordCommand userResetPasswordCommand)
        {
            var actionName = ControllerContext.ActionDescriptor.ActionName;
            try
            {
                var user = await userManager.FindByEmailAsync(userResetPasswordCommand.Username);
                if(user!=null)
                {
                    var code = await userManager.GeneratePasswordResetTokenAsync(user);
                }     
                else
                {

                }
                return Ok();
            }
            catch 
            {
                return StatusCode(internalServerErrorCode, _configuration.DisplayGenericUserErrorMessage);
            }
        }
    }
}
