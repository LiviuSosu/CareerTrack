using CareerTrack.Application.Articles.Commands.Update;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Articles.Command
{
    public class UpdateArticleCommandHandlerTests
    {
        private CareerTrackDbContext db;

        public UpdateArticleCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<CareerTrackDbContext>()
            .UseInMemoryDatabase(databaseName: "UpdateCareerTrackArticle")
            .Options;

            db = new CareerTrackDbContext(options);
            db.Articles.AddRange(new[] {
                new Article {
                    Id = Guid.Parse("8464B045-6F16-4A73-7E41-08D690385B3B"),
                    Name = "Article 1",
                    Link = "www.link1.com"
                    },
                new Article {
                    Id = Guid.Parse("8FD637BF-53E6-41B9-7E42-08D690385B3B"),
                    Name = "Article 2",
                    Link = "www.link2.com"
                    },
                new Article {
                    Id = Guid.Parse("FEA44EA2-1D4C-49BB-92A0-1AD6899CA220"),
                    Name = "Article 3",
                    Link = "www.link3.com"
                    }
                });

            db.SaveChanges();
        }

        [Fact]
        public async Task UpdateArticleTest()
        {
            var id = Guid.Parse("8FD637BF-53E6-41B9-7E42-08D690385B3B");
            var sut = new UpdateArticleCommandHandler(db);

            var result = await sut.Handle(new UpdateArticleCommand
            {
                Id = id,
                Name = "Article Update",
                Link = "www.link2update.com"
            },
            CancellationToken.None);

            result.ShouldBeOfType<Unit>();
            var article = db.Articles.Find(id);
            article.ShouldNotBeNull();
            article.Name.ShouldBe("Article Update");
            article.Link.ShouldBe("www.link2update.com");
        }
    }
}
