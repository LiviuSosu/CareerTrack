using CareerTrack.Application.Handlers.Articles.Commands.Delete;
using CareerTrack.Application.Tests.Articles.Query;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Articles.Command
{
    public class DeleteArticleTest : ArticlesTest
    {
        public DeleteArticleTest()
        {
        }

        [Fact]
        public async Task DeleteArticleSuccessTest()
        {
            db = new CareerTrackDbContext(options);
            var artcileId = Guid.NewGuid();
            db.Articles.Add(new Article
            {
                Id = artcileId,
                Link = "www.link.com",
                Title = "Some Title"
            });
            db.Articles.RemoveRange(db.Articles);
            db.SaveChanges();

            db = new CareerTrackDbContext(options);
            var deleteArticleCommand = new DeleteArticleCommand
            {
                Id = artcileId
            };

            var sut = new DeleteArticleCommandHandler(db);
            var result = await sut.Handle(deleteArticleCommand, CancellationToken.None);

            var art = await db.Articles.AsNoTracking()
                .SingleOrDefaultAsync(a => a.Id == artcileId);
            Assert.Null(art);

            db.Articles.RemoveRange(db.Articles);
            db.SaveChanges();
            await db.DisposeAsync();
        }

        [Fact]
        public async Task DeleteArticleFail_WhenArticleDoesNotExist()
        {
            db = new CareerTrackDbContext(options);
            var deleteArticleCommand = new DeleteArticleCommand
            {
                Id = Guid.NewGuid()
            };

            var sut = new DeleteArticleCommandHandler(db);

            _ = await Assert.ThrowsAsync<DbUpdateConcurrencyException>(() => sut.Handle(deleteArticleCommand, CancellationToken.None));

            await db.DisposeAsync();
        }
    }
}
