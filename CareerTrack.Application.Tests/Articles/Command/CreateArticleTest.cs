using CareerTrack.Application.Handlers.Articles.Commands.Create;
using CareerTrack.Application.Tests.Articles.Query;
using CareerTrack.Persistance;
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
        const string articleTitle = "My Article";
        const string articleLink = "www.myarticle.com";
        public CreateArticleTest()
        {
            createArticleCommand = new CreateArticleCommand
            {
                Link = articleLink,
                Title = articleTitle
            };
        }

        [Fact]
        public async Task CreateArticleSuccessTest()
        {
            db = new CareerTrackDbContext(options);
            db.Articles.RemoveRange(db.Articles);
            db.SaveChanges();

            var sut = new CreateArticleCommandHandler(db);
            var result = await sut.Handle(createArticleCommand, CancellationToken.None);

            var newArticle = db.Articles.FirstOrDefault();

            Assert.IsType<Unit>(result);

            Assert.NotNull(newArticle);
            //Assert.Equal(1, db.Articles.Count());
            Assert.Equal(articleTitle, newArticle.Title);
            Assert.Equal(articleLink, newArticle.Link);
        }
    }
}
