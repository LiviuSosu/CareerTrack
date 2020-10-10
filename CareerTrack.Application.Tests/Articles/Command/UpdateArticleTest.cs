using CareerTrack.Application.Handlers.Articles.Commands.Update;
using CareerTrack.Application.Tests.Articles.Query;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Articles.Command
{
    public class UpdateArticleTest : ArticlesTest
    {
        const string articleLinkToModify = "www.modified-link.com";

        [Fact]
        public async Task UpdateArticleSuccessTest()
        {
            options = new DbContextOptionsBuilder<CareerTrackDbContext>()
                     .UseInMemoryDatabase(databaseName: "CareerTrackUpdateArticles").Options;

            db = new CareerTrackDbContext(options);
            db.Articles.RemoveRange(db.Articles);
            db.SaveChanges();

            var artcileId = Guid.NewGuid();
            db.Articles.Add(new Article
            {
                Id = artcileId,
                Link = "www.link.com",
                Title = "Some Title"
            });
            db.SaveChanges();
            db.Dispose();

            db = new CareerTrackDbContext(options);

            var sut = new UpdateArticleCommandHandler(db);
            var updateArticleCommand = new UpdateArticleCommand
            { Id = artcileId,
                Link = articleLinkToModify
            };
            var result = await sut.Handle(updateArticleCommand, CancellationToken.None);

            var art = await db.Articles.SingleOrDefaultAsync(a => a.Id == artcileId);

            Assert.Equal(articleLinkToModify, art.Link);
        }


        [Fact]
        public async Task UpdateArticleFail_WhenArticleDoesNotExist()
        {
            var articleId = Guid.Parse("FEA44EA2-1D4C-49AC-92A0-1AD6899CA220");

            var updateArticleCommand = new UpdateArticleCommand
            {
                Id = articleId,
            };

            options = new DbContextOptionsBuilder<CareerTrackDbContext>()
                    .UseInMemoryDatabase(databaseName: "CareerTrackUpdateArticles").Options;

            db = new CareerTrackDbContext(options);
            var sut = new UpdateArticleCommandHandler(db);

            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(() => sut.Handle(updateArticleCommand, CancellationToken.None));

            db.Articles.RemoveRange(db.Articles);
        }
    }
}
