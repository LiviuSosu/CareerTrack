using CareerTrack.Application.Handlers.Articles.Commands.Update;
using CareerTrack.Application.Handlers.Articles.Queries.GetArticle;
using CareerTrack.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Articles.Command
{
    public class UpdateCommandAticleTest : ArticlesTest
    {
        [Fact]
        public async Task UpdateArticleSuccessTest()
        {
            var queryHandler = new GetArticleQueryHandler(db);
            var getArticleQuery = new GetArticleQuery(firstArticleId);
            var article = await queryHandler.Handle(getArticleQuery, CancellationToken.None);

            db = new CareerTrackDbContext(options);

            var sut = new UpdateArticleCommandHandler(db);
            var updatedArticleCommand = new UpdateArticleCommand
            {
                Id = firstArticleId,
                Title = "Updated Article Title",
                Link = article.Link,
            };

            var result = await sut.Handle(updatedArticleCommand, CancellationToken.None);

            var updatedArticle = queryHandler.Handle(getArticleQuery, CancellationToken.None);

            Assert.IsType<Unit>(result);
           Assert.NotNull(updatedArticle);
        }

        [Fact]
        public async Task UpdateArticleFail_When_Article_Does_Not_Exist()
        {
            var updateArticleCommand = new UpdateArticleCommand
            {
                Id = nonExistingArticleId,
            };

            var sut = new UpdateArticleCommandHandler(db);

            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(() => sut.Handle(updateArticleCommand, CancellationToken.None));
        }
    }
}
