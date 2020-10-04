using CareerTrack.Application.Exceptions;
using CareerTrack.Application.Handlers.Users.Commands.ChangePassword;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Users.Command
{
    public class UserChangePasswordCommandTest : UsersTest
    {
        [Fact]
        public async Task UserChangePasswordCommand_UserNameNotFound_Test()
        {
            var sut = new ChangePasswordCommandHandler(db);
            var userChangePasswordCommand = new UserChangePasswordCommand
            {
                Username = "SomeUsername",
                OldPassword = "SomePassword200!",
                NewPassword = "SomePassword200/",
                ConfirmPassword = "SomePassword200/",
                UserManager = mgr.Object
            };

            await Assert.ThrowsAsync<NotFoundException>(() => sut.Handle(userChangePasswordCommand, CancellationToken.None));
        }

        [Fact]
        public async Task UserChangePasswordCommand_PasswordsAreNotTheSame_Test()
        {
            var sut = new ChangePasswordCommandHandler(db);
            var userChangePasswordCommand = new UserChangePasswordCommand
            {
                Username = "SomeUsername",
                OldPassword = "SomePassword200!",
                NewPassword = "SomePassword200/",
                ConfirmPassword = "SomePassword200$",
                UserManager = mgr.Object
            };

            await Assert.ThrowsAsync<PasswordsAreNotTheSameException>(() => sut.Handle(userChangePasswordCommand, CancellationToken.None));
        }
    }
}
