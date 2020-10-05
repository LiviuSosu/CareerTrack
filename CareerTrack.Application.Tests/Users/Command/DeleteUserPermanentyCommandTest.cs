using CareerTrack.Application.Exceptions;
using CareerTrack.Application.Handlers.Users.Commands.DeletePermanenty;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance.Repository.UserRoleRepository;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Users.Command
{
    public class DeleteUserPermanentyCommandTest : UsersTest
    {
        public DeleteUserPermanentyCommandTest()
        {
            store = new Mock<IUserStore<User>>();
            mgr = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            mgr.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success)
                    .Callback<User, string>((userToAdd, _) => db.Users.Add(userToAdd));
        }
        [Fact]
        public async Task DeleteUserPermanentyCommandSuccessTest()
        {
            const string usernameToDelete = "admin3";
            var user = new User()
            {
                UserName = usernameToDelete,
                Email = "admin3@test.com"
            };
            mgr.Setup(x => x.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

            db.Users.Add(user);
            await mgr.Object.CreateAsync(user, "SomePassword123!");

            db.SaveChanges();
            var users = db.Users;
            db.UserRoles.Add(new IdentityUserRole<Guid>
            {
                UserId = db.Users.Where(u => u.UserName == usernameToDelete)
                .SingleOrDefault().Id,
                RoleId = Guid.Parse("3B246FA9-9B03-42B1-A06D-BFDE47AA6868")
            });
            db.SaveChanges();

            var repository = new Mock<IUserRoleRepository>();
            var userRoleRepositoryMock = new Mock<IUserRoleRepository>();

            mgr.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);

            var deleteUserPermanentyCommand = new DeleteUserPermanentyCommand
            {
                Username = usernameToDelete,
                UserManager = mgr.Object
            };

            var sut = new DeleteUserPermanentlyCommandHandler(db);
            var result = await sut.Handle(deleteUserPermanentyCommand, CancellationToken.None);
            Assert.Null(mgr.Object.Users.Where(u => u.UserName == usernameToDelete).FirstOrDefault());

            db.Users.RemoveRange(db.Users);
        }

        [Fact]
        public async Task DeleteUserPermanentyCommandFail_WhenNotFoundException_Test()
        {
            var user = new User()
            {
                UserName = username,
                Email = email
            };
            mgr.Setup(x => x.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success)
                .Callback<User>(x => db.Users.Remove(x));

            var deleteUserPermanentyCommand = new DeleteUserPermanentyCommand
            {
                Username = "someNonexistingUser"
            };

            var sut = new DeleteUserPermanentlyCommandHandler(db);
            await Assert.ThrowsAsync<NotFoundException>(() => sut.Handle(deleteUserPermanentyCommand, CancellationToken.None));
        }
    }
}
