using CareerTrack.Application.Handlers.Articles.Queries;
using CareerTrack.Application.Handlers.Articles.Queries.GetArticle;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Articles.Query
{
    public class GetArticleQueryTest : ArticlesTest
    {
        [Fact]
        public async Task GetArticleQueryHandlerTest_Succes()
        {
            var sut = new GetArticleQueryHandler(db);

            var getArticleQuery = new GetArticleQuery(firstArticleId);

            var result = await sut.Handle(getArticleQuery, CancellationToken.None);

            Assert.IsType<ArticleLookupModel>(result);
            Assert.NotNull(result);
            Assert.Equal("www.link1.com", result.Link);
        }

        [Fact]
        public async Task GetArticleQueryHandlerTest_Fail_When_Id_Doe_Not_Exist()
        {
            var sut = new GetArticleQueryHandler(db);

            var getArticleQuery = new GetArticleQuery(nonExistingArticleId);

            var result = await sut.Handle(getArticleQuery, CancellationToken.None);

            Assert.Null(result);
        }
    }
}
