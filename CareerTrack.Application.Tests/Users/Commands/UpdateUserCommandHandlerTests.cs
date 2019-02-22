//using CareerTrack.Application.Exceptions;
//using CareerTrack.Application.Tests.Users.Queries;
//using CareerTrack.Application.Users.Commands.UpdateCustomer;
//using MediatR;
//using Shouldly;
//using System;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using Xunit;

//namespace CareerTrack.Application.Tests.Users.Commands
//{
//    public class UpdateUserCommandHandlerTests : UsersTestsBase
//    {
//        [Fact, TestPriority(-5)]
//        public async Task UpdateUserTest()
//        {
//            db.Users.ToList()[2].UserName.ShouldBe("BrendanRichards");
//            var sut = new UpdateUserCommandHandler(db);     
//             var result = await sut.Handle(new UpdateUserCommand() { Id = Guid.Parse("FEA44EA2-1D4C-49BB-92A0-1AD6899CA220"), UserName = "JamesBond" }, CancellationToken.None);

//             result.ShouldBeOfType<Unit>();

//            db.Users.ToList()[2].UserName.ShouldBe("JamesBond");
//        }

//        [Fact]
//        public async Task UpdateUser_Expect_Exception_When_UserID_Does_Not_Exist()
//        {
//            var sut = new UpdateUserCommandHandler(db);
//            await Assert.ThrowsAsync<NotFoundException>(() =>
//                sut.Handle(new UpdateUserCommand() { Id = Guid.Parse("8464B045-6F16-4A73-7E41-08D690385B3C") }, CancellationToken.None));
//        }
//    }
//}
