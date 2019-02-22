//using CareerTrack.Application.Users.Queries.GetUsersList;
//using Shouldly;
//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using Xunit;

//namespace CareerTrack.Application.Tests.Users.Queries
//{
//    public class GetUsersListQueryHandlerTests : UsersTestsBase
//    {       
//        [Fact]
//        public async Task GetFilteredUsersTest()
//        {
//            var sut = new GetUsersListQueryHandler(db);
//            pagingModel.QueryFilter = "D";

//            var result = await sut.Handle(new GetUsersListQuery(pagingModel), CancellationToken.None);

//            result.ShouldBeOfType<UsersListViewModel>();
//            result.Users.Count.ShouldBe(2);
//        }

//        [Fact, TestPriority(-5)]
//        public async Task GetFirstPageTest()
//        {
//            var sut = new GetUsersListQueryHandler(db);
//            pagingModel.PageSize = Int16.MaxValue;

//            var result = await sut.Handle(new GetUsersListQuery(pagingModel), CancellationToken.None);

//            result.ShouldBeOfType<UsersListViewModel>();
//            result.Users.Count.ShouldBe(3);
//        }
//    }
//}
