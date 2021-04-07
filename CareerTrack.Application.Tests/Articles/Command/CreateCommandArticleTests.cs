using CareerTrack.Application.Handlers.Articles.Commands.Create;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Articles.Command
{
    public class CreateCommandArticleTests : ArticlesTest
    {
        const string newTitleArticle = "New Article Title";
        [Fact]
        public async Task CreateArticleSuccessTest()
        {
            var sut = new CreateArticleCommandHandler(db);
            var createArticleCommand = new CreateArticleCommand
            {
                Title = newTitleArticle,
                Link = "www.NewLink.com"
            };

            var result = await sut.Handle(createArticleCommand, CancellationToken.None);
            var createdArticle = db.Articles.Where(u => u.Title == newTitleArticle);

            Assert.IsType<Unit>(result);
            Assert.NotNull(createdArticle);
        }
    }
}
