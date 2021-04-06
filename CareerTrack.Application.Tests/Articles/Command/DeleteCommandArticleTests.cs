using CareerTrack.Application.Handlers.Articles.Commands.Delete;
using CareerTrack.Application.Handlers.Articles.Queries.GetArticle;
using CareerTrack.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Articles.Command
{
    public class DeleteCommandArticleTests : ArticlesTest
    {
        [Fact]
        public async Task DeleteArticleSuccessTest_Success()
        {
            db = new CareerTrackDbContext(options);

            var sut = new DeleteArticleCommandHandler(db);
            var updatedArticleCommand = new DeleteArticleCommand
            {
                Id = secondArticleId
            };

            var result = await sut.Handle(updatedArticleCommand, CancellationToken.None);

            Assert.IsType<Unit>(result);
        }

        [Fact]
        public async Task DeleteArticleSuccessTest_Fail_When_Article_Does_Not_Exist()
        {
            var updateArticleCommand = new DeleteArticleCommand
            {
                Id = nonExistingArticleId,
            };

            var sut = new DeleteArticleCommandHandler(db);

            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(() => sut.Handle(updateArticleCommand, CancellationToken.None));
        }
    }
}
