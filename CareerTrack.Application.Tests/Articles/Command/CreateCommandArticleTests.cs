using CareerTrack.Application.Handlers.Articles.Commands.Create;
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
    public class CreateCommandArticleTests : ArticlesTest
    {
        [Fact]
        public async Task CreateArticleSuccessTest()
        {
            var sut = new CreateArticleCommandHandler(db);
            var createArticleCommand = new CreateArticleCommand
            {
                Title = "New Article Title",
                Link = "www.NewLink.com"
            };

            var result = await sut.Handle(createArticleCommand, CancellationToken.None);
            var createdArticle = db.Articles.Where(u => u.Title == "New Article Title");

            Assert.IsType<Unit>(result);
            Assert.NotNull(createdArticle);
        }
    }
}
