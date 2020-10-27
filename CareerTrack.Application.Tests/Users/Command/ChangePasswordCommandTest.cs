using CareerTrack.Application.Exceptions;
using CareerTrack.Application.Handlers.Users.Commands.ChangePassword;
using CareerTrack.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Users.Command
{
    public class ChangePasswordCommandTest : UsersTest
    {
        [Fact]
        public async Task UserChangePasswordSuccess()
        {
            store = new Mock<IUserStore<User>>();
            mgr = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            mgr.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success)
                    .Callback<User, string>((userToAdd, _) => db.Users.Add(userToAdd));

            const string usernameToDelete = "admin4";
            var user = new User()
            {
                UserName = usernameToDelete,
                Email = "admin4@test.com"
            };

            mgr.Setup(x => x.FindByNameAsync(usernameToDelete)).ReturnsAsync(user);
            mgr.Setup(x => x.ChangePasswordAsync(user, It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            var sut = new ChangePasswordCommandHandler(db);
            var userChangePasswordCommand = new ChangePasswordCommand
            {
                Username = usernameToDelete,
                OldPassword = "SomePassword200!",
                NewPassword = "SomePassword200/",
                ConfirmPassword = "SomePassword200/",
                UserManager = mgr.Object
            };

            Assert.IsType<Unit>(await sut.Handle(userChangePasswordCommand, CancellationToken.None));
        }

        [Fact]
        public async Task UserChangePasswordCommand_UserNameNotFound_Test()
        {
            var sut = new ChangePasswordCommandHandler(db);
            var userChangePasswordCommand = new ChangePasswordCommand
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
            var userChangePasswordCommand = new ChangePasswordCommand
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
