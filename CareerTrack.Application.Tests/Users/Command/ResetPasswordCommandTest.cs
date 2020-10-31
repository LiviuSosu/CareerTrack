using CareerTrack.Application.Exceptions;
using CareerTrack.Application.Handlers.Users.Commands.ResetPassword;
using CareerTrack.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Users.Command
{
    public class ResetPasswordCommandTest : UsersTest
    {
        const string token = "SomeToken";
        User user = new User()
        {
            UserName = "user",
            Email = "user@test.com"
        };

        public ResetPasswordCommandTest()
        {
            store = new Mock<IUserStore<User>>();
            mgr = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            mgr.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success)
                    .Callback<User, string>((userToAdd, _) => db.Users.Add(userToAdd));

            mgr.Setup(x => x.FindByNameAsync(user.UserName)).ReturnsAsync(user);
            mgr.Setup(x => x.ResetPasswordAsync(user, token, It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
        }

        [Fact]
        public async Task ResetPasswordCommandSuccess()
        {     
            var sut = new ResetPasswordCommandHandler(db);
            var userResetPasswordCommand = new UserResetPasswordCommand
            {
                Username = user.UserName,
                Token = token,
                NewPassword = "NewPassword123!",
                ConfirmPassword = "NewPassword123!",
                UserManager = mgr.Object
            };

            Assert.IsType<Unit>(await sut.Handle(userResetPasswordCommand, CancellationToken.None));
        }

        [Fact]
        public async Task UserResetPasswordCommand_UserNameNotFound_Test()
        {
            var sut = new ResetPasswordCommandHandler(db);
            var userResetPasswordCommand = new UserResetPasswordCommand
            {
                Username = "SomeUsername",
                Token = token,
                NewPassword = "SomePassword200/",
                ConfirmPassword = "SomePassword200/",
                UserManager = mgr.Object
            };

            await Assert.ThrowsAsync<NotFoundException>(() => sut.Handle(userResetPasswordCommand, CancellationToken.None));
        }

        [Fact]
        public async Task UserResetPasswordCommand_PasswordsAreNotTheSame_Test()
        {
            var sut = new ResetPasswordCommandHandler(db);
            var userChangePasswordCommand = new UserResetPasswordCommand
            {
                Username = user.UserName,
                Token = token,
                NewPassword = "SomePassword200/",
                ConfirmPassword = "SomePassword200!",
                UserManager = mgr.Object
            };

            await Assert.ThrowsAsync<PasswordsAreNotTheSameException>(() => sut.Handle(userChangePasswordCommand, CancellationToken.None));
        }
    }
}
