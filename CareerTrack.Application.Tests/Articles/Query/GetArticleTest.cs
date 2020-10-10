using CareerTrack.Application.Handlers.Articles.Queries;
using CareerTrack.Application.Handlers.Articles.Queries.GetArticle;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Articles.Query
{
    public class GetArticleTest : ArticlesTest
    {
        [Fact]
        public async Task GetArticleByIdTest()
        {
            dbReader = InitializeDatabase("GetArticleByIdTest");
            var sut = new GetArticleQueryHandler(dbReader);

            var result = await sut.Handle(new GetArticleQuery(articleIdForTheSecondArticle), CancellationToken.None);

            Assert.IsType<ArticleLookupModel>(result);
            Assert.Equal(articleTitleForTheSecondArticle, result.Title);
        }
    }
}
