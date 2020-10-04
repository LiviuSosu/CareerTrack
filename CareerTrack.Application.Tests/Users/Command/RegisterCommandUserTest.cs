using CareerTrack.Application.Handlers.Users.Commands.Register;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Users.Command
{
    public class RegisterCommandUserTest : UsersTest
    {
        [Fact]
        public async Task RegisterUserSuccessTest()
        {
            db.Users.RemoveRange(db.Users);

            var sut = new UserRegisterCommandHandler(db);
            var userRegisterCommand = new UserRegisterCommand
            {
                Username = "SomeUsername",
                Email = "some.email@domain.com",
                Password = "SomePassword200/",
                RoleId = Guid.Parse("2BAF6E28-B1C9-42FE-953F-3B660BEFC6DA"),
                UserManager = mgr.Object
            };

            var result = await sut.Handle(userRegisterCommand, CancellationToken.None);
            var registerdUser = db.Users.Where(u => u.UserName == "SomeUsername");

            Assert.IsType<Unit>(result);
            Assert.NotNull(registerdUser);

            db.Users.RemoveRange(db.Users);
        }
    }
}
