﻿using CareerTrack.Application.Handlers.Articles.Commands.Delete;
using CareerTrack.Application.Tests.Articles.Query;
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
        DeleteArticleCommand deleteArticleCommand;

        public DeleteArticleTest()
        {
            deleteArticleCommand = new DeleteArticleCommand
            {
                Id = articleIdForTheThirdArticle
            };
        }

        [Fact]
        public async Task DeleteArticleSuccessTest()
        {
            var sut = new DeleteArticleCommandHandler(db);

            var art = await db.Articles.AsNoTracking()
              .SingleOrDefaultAsync(a => a.Id == articleIdForTheThirdArticle);

            db.Entry(art).State = EntityState.Detached;
            Unit result;
            try
            {
                result = await sut.Handle(deleteArticleCommand, CancellationToken.None);
            }
            catch (InvalidOperationException)
            {
                db.Entry(art).State = EntityState.Detached;
                await DeleteArticleSuccessTest();
            }
            art = await db.Articles.AsNoTracking()
                .SingleOrDefaultAsync(a => a.Id == articleIdForTheThirdArticle);

            Assert.Null(art);
        }


        [Fact]
        public async Task DeleteArticleFail_WhenArticleDoesNotExist()
        {
            var articleId =  Guid.Parse("FEA44EA2-1D4C-49AC-92A0-1AD6899CA220");
            var sut = new DeleteArticleCommandHandler(db);

            var art = await db.Articles.AsNoTracking()
              .SingleOrDefaultAsync(a => a.Id == articleId);

            try
            {
                _ = await Assert.ThrowsAsync<InvalidOperationException>(() => sut.Handle(deleteArticleCommand, CancellationToken.None));
            }
            catch (InvalidOperationException)
            {
                db.Entry(art).State = EntityState.Detached;
                await DeleteArticleSuccessTest();
            }
            art = await db.Articles.AsNoTracking()
                .SingleOrDefaultAsync(a => a.Id == articleId);

            Assert.Null(art);
        }
    }
}