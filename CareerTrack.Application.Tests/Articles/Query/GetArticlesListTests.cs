using CareerTrack.Application.Handlers.Articles.Queries.GetArticles;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Articles.Query
{
    public class GetArticlesListTests : ArticlesTest
    {      
        [Fact]
        public async Task GetFilteredArticlesTest()
        {
            var sut = new GetArticlesListQueryHandler(db);
            pagingModel.QueryFilter = "2";

            var result = await sut.Handle(new GetArticlesListQuery(pagingModel), CancellationToken.None);

            Assert.IsType<ArticlesListViewModel>(result);
            Assert.Equal(1, result.Articles.Count);

            db.Articles.RemoveRange(db.Articles);
        }
    }
}
