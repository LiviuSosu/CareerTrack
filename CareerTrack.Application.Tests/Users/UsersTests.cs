using CareerTrack.Application.Users.Commands.CreateUser;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Users
{
    public class UsersTests : UsersTestsBase
    {
        [Fact(Skip = "Could not mock user manager")]
        public async Task RegisterUser()
        {
            var sut = new CreateUserCommandHandler(db);

            var serviceProviderMock = new Mock<IServiceProvider>();

            serviceProviderMock.Setup(x => x.GetService(typeof(CareerTrackDbContext)))
            .Returns(db);

            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProviderMock.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory.Setup(x => x.CreateScope())
                .Returns(serviceScope.Object);

            var mockUserStore = new Mock<IUserStore<IdentityUser<Guid>>>();

            var result = await sut.Handle(new CreateUserCommand
            {
                UserName = "JohnDoe",
                UserEmail = "email@smth.com",
                Password = "Password123",
                ServiceProvider = serviceProviderMock.Object,
                UserManager = null

            }, CancellationToken.None);

            result.ShouldBeOfType<Unit>();
        }
    }
}
