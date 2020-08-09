using CareerTrack.Application.Articles.Queries.GetArticle;
using CareerTrack.Application.Articles.Queries.GetArticles;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Articles.Query
{
    public class GetArticleTest : GetArticlesTest
    {
        [Fact]
        public async Task GetArticleByIdTest()
        {
            var sut = new GetArticleQueryHandler(db);

            var result = await sut.Handle(new GetArticleQuery(articleIdForTheSecondArticle), CancellationToken.None);

            Assert.IsType<ArticleLookupModel>(result);
            Assert.Equal(articleTitleForTheSecondArticle, result.Title);
        }
    }
}
