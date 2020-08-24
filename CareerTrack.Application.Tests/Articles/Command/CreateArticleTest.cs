using CareerTrack.Application.Handlers.Articles.Commands.Create;
using CareerTrack.Application.Tests.Articles.Query;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Articles.Command
{
    public class CreateArticleTest : ArticlesTest
    {
        CreateArticleCommand createArticleCommand;
        string articleTitle;
        public CreateArticleTest()
        {
            articleTitle = "My Article";
            createArticleCommand = new CreateArticleCommand
            {
                Link = "www.myarticle.com",
                Title = articleTitle
            };
        }

        [Fact]
        public async Task CreateArticleSuccessTest()
        {
            var sut = new CreateArticleCommandHandler(db);
            var nrOfArticlesBefore =  db.Articles.Count();
            var result = await sut.Handle(createArticleCommand, CancellationToken.None);
            var nrOfArticleAfter =  db.Articles.Count();

            var newArticle = db.Articles.Where(a=>a.Title == articleTitle).FirstOrDefault();

            Assert.IsType<Unit>(result);

            Assert.NotNull(newArticle);
            Assert.Equal(articleTitle, newArticle.Title);
        }
    }
}
