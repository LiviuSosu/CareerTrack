using CareerTrack.Application.Handlers.Articles.Commands.Update;
using CareerTrack.Application.Tests.Articles.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Articles.Command
{
    public class UpdateArticleTest : ArticlesTest
    {
        UpdateArticleCommand updateArticleCommand;
        const string articleLinkToModify = "www.modified-link.com";
        public UpdateArticleTest()
        {
            updateArticleCommand = new UpdateArticleCommand
            {
                Link = articleLinkToModify
            };       
        }

        [Fact]
        public async Task UpdateArticleSuccessTest()
        {
            var sut = new UpdateArticleCommandHandler(db);
            var art = await db.Articles.AsNoTracking()
                .SingleOrDefaultAsync(a=>a.Id== articleIdForTheFirstArticle);

            var oldCopy = art;

            updateArticleCommand.Id = articleIdForTheFirstArticle;
            updateArticleCommand.Title = oldCopy.Title;
            db.Entry(art).State = EntityState.Detached;
            Unit result;
            try
            {
                 result = await sut.Handle(updateArticleCommand, CancellationToken.None);
            }
            catch(InvalidOperationException)
            {
                db.Entry(art).State = EntityState.Detached;
                await UpdateArticleSuccessTest();
            }
            art = await db.Articles.AsNoTracking()
                .SingleOrDefaultAsync(a => a.Id == articleIdForTheFirstArticle);

            Assert.Equal(articleLinkToModify, art.Link);
            Assert.Equal(oldCopy.Title, art.Title);
        }

        [Fact]
        public async Task UpdateArticleFail_WhenArticleDoesNotExist()
        {
            var articleId = Guid.Parse("FEA44EA2-1D4C-49AC-92A0-1AD6899CA220");
            updateArticleCommand.Id = articleId;
            var sut = new UpdateArticleCommandHandler(db);

            var art = await db.Articles.AsNoTracking()
              .SingleOrDefaultAsync(a => a.Id == articleId);
            try
            {
                _ = await Assert.ThrowsAsync<DbUpdateConcurrencyException>(() => sut.Handle(updateArticleCommand, CancellationToken.None));
            }
            catch (DbUpdateConcurrencyException)
            {
                //db.Entry(art).State = EntityState.Detached;
                //await UpdateArticleFail_WhenArticleDoesNotExist();
            }
            //art = await db.Articles.AsNoTracking()
            //    .SingleOrDefaultAsync(a => a.Id == articleId);

            //Assert.Null(art);
        }
    }
}
