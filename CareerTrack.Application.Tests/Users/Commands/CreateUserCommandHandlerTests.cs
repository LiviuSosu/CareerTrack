using CareerTrack.Application.Tests.Users.Queries;
using CareerTrack.Application.Users.Commands.CreateUser;
using MediatR;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Users.Commands
{
    public class CreateUserCommandHandlerTests : UsersTestsBase
    {
        [Fact, TestPriority(-5)]
        public async Task CreateValidUserTest()
        {
            var sut = new CreateUserCommandHandler(db, null);

            var result = await sut.Handle(new CreateUserCommand() { UserName = "Test" }, CancellationToken.None);

            result.ShouldBeOfType<Unit>();
        }
    }
}
