//using CareerTrack.Application.Exceptions;
//using CareerTrack.Application.Tests.Users.Queries;
//using CareerTrack.Application.Users.Commands.DeleteUser;
//using MediatR;
//using Shouldly;
//using System;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using Xunit;

//namespace CareerTrack.Application.Tests.Users.Commands
//{
//    public class DeleteUserCommandHandlerTests : UsersTestsBase
//    {
//        [Fact, TestPriority(5)]
//        public async Task DeleteUserTest()
//        {
//            var sut = new DeleteUserCommandHandler(db);

//            var result = await sut.Handle(new DeleteUserCommand() { Id = Guid.Parse("8464B045-6F16-4A73-7E41-08D690385B3B") }, CancellationToken.None);

//            result.ShouldBeOfType<Unit>();
//            db.Users.ToList().Count.ShouldBe(3);
//        }

//        [Fact]
//        public async Task DeleteUser_Expect_Exception_When_UserId_Does_Not_Exist() 
//        {
//            var sut = new DeleteUserCommandHandler(db);
//            await Assert.ThrowsAsync<NotFoundException>( ()=>
//                 sut.Handle(new DeleteUserCommand() { Id = Guid.Parse("8464B045-6F16-4A73-7E41-08D690385B3A") }, CancellationToken.None));
//        }
//    }
//}
