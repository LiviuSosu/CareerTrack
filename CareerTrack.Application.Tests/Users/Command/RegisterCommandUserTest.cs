using CareerTrack.Application.Handlers.Users.Commands.Register;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance.Repository.UserRoleRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Moq;
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
            //https://stackoverflow.com/questions/49165810/how-to-mock-usermanager-in-net-core-testing
            var store = new Mock<IUserStore<User>>();
            var mgr = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
           
            var sut = new UserRegisterCommandHandler(null,null,db);
            throw new NotImplementedException();
            var userRegisterCommand = new UserRegisterCommand
            {
                Username = "SomeUsername",
                Email="some.email@domain.com",
                Password="SomePassword200/",
                RoleId= Guid.Parse("2BAF6E28-B1C9-42FE-953F-3B660BEFC6DA"),
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
