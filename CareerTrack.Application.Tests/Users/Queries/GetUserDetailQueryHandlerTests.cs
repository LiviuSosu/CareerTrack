using CareerTrack.Application.Users.Queries.GetUserDetail;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Users.Queries
{
    public class GetUserDetailQueryHandlerTests : UsersTestsBase
    {
        [Fact]
        public async Task GetFirstUser()
        {
            var sut = new GetUserDetailQueryHandler(db);

            var result = await sut.Handle(new GetUserDetailQuery() { Id = Guid.Parse("8464B045-6F16-4A73-7E41-08D690385B3B") }, CancellationToken.None);

            result.ShouldBeOfType<UserDetailModel>();
            result.UserName.ShouldBe("AdamCogan");
        }
    }
}
