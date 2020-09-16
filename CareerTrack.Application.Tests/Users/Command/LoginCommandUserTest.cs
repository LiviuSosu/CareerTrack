using CareerTrack.Application.Exceptions;
using CareerTrack.Application.Handlers.Users.Commands.Login;
using CareerTrack.Common;
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
    public class LoginCommandUserTest : UsersTest
    {
        [Fact]
        public async Task LoginUserSuccessTest()
        {
            //https://stackoverflow.com/questions/49165810/how-to-mock-usermanager-in-net-core-testin  
            var store = new Mock<IUserStore<User>>();
            var mgr = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            var user = new User()
            {
                UserName = "admin2",
                Email = "admin@b.com"
            };
            mgr.Setup(x => x.CheckPasswordAsync(user, "Password@123")).ReturnsAsync(true);
            mgr.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<User, string>((x, y) => db.Users.Add(x));


            db.Users.Add(user);
            await mgr.Object.CreateAsync(user, "Password123!");

            db.SaveChanges();
            var users = db.Users;
            db.UserRoles.Add(new IdentityUserRole<Guid> { UserId = db.Users.FirstOrDefault().Id, RoleId = Guid.Parse("3B246FA9-9B03-42B1-A06D-BFDE47AA6868") });
            db.SaveChanges();

            var repository = new Mock<IUserRoleRepository>();
            var userRoleRepositoryMock = new Mock<IUserRoleRepository>();

            mgr.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
          
            var jwtConfiguration = new JWTConfiguration("MySuperSecureKey", "CareerTrack", "CareerTrack", "24", "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");

            var userLoginCommand = new UserLoginCommand
            {
                Username = "admin2",
                Password = "Password@123",
                UserManager = mgr.Object,
                JWTConfiguration = jwtConfiguration
            };

            var sut = new UserLoginCommandHandler(db);
            var result = await sut.Handle(userLoginCommand, CancellationToken.None);

            Assert.IsType<LoginResponseDto>(result);

            db.Users.RemoveRange(db.Users);
        }

        [Fact]
        public async Task LoginUserFailedTest()
        {
            var store = new Mock<IUserStore<User>>();
            var mgr = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            var user = new User()
            {
                UserName = "admin2",
                Email = "admin@b.com"
            };
            mgr.Setup(x => x.CheckPasswordAsync(user, "Password@123")).ReturnsAsync(true);
            mgr.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<User, string>((x, y) => db.Users.Add(x));

            var repository = new Mock<IUserRoleRepository>();
            var userRoleRepositoryMock = new Mock<IUserRoleRepository>();

            mgr.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);

            var jwtConfiguration = new JWTConfiguration("MySuperSecureKey", "CareerTrack", "CareerTrack", "24", "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");

            var userLoginCommand = new UserLoginCommand
            {
                Username = "admin2",
                Password = "Password123!",
                UserManager = mgr.Object,
                JWTConfiguration = jwtConfiguration
            };

            var sut = new UserLoginCommandHandler(db);

            await Assert.ThrowsAsync<LoginFailedException>(()=>sut.Handle(userLoginCommand, CancellationToken.None));

            db.Users.RemoveRange(db.Users);
        }
    }
}
