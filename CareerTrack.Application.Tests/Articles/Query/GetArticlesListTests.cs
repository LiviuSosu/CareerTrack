using CareerTrack.Application.Articles.Queries.GetArticles;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Articles.Query
{
    public class GetArticlesListTests : GetArticlesTest
    {      
        [Fact]
        public async Task GetFilteredArticlesTest()
        {
            var sut = new GetArticlesListQueryHandler(db);
            pagingModel.QueryFilter = "2";

            var result = await sut.Handle(new GetArticlesListQuery(pagingModel), CancellationToken.None);

            Assert.IsType<ArticlesListViewModel>(result);
            Assert.Equal(1, result.Articles.Count);
            //result.ShouldBeOfType<ArticlesListViewModel>();
            //result.Articles.Count.ShouldBe(1);
        }
    }
}
