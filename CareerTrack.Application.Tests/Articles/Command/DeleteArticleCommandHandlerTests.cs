using CareerTrack.Application.Articles.Commands.Delete;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Articles.Command
{
    public class DeleteArticleCommandHandlerTests
    {
        private CareerTrackDbContext db;

        public DeleteArticleCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<CareerTrackDbContext>()
            .UseInMemoryDatabase(databaseName: "DeleteCareerTrackArticle")
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
        public async Task DeleteArticleTest()
        {
            var id = Guid.Parse("8FD637BF-53E6-41B9-7E42-08D690385B3B");
            var sut = new DeleteArticleCommandHandler(db);

            var result = await sut.Handle(new DeleteArticleCommand
            {
                Id = id
            },
            CancellationToken.None);

            result.ShouldBeOfType<Unit>();
            db.Articles.Count().ShouldBe(2);
            db.Articles.Find(id).ShouldBeNull();
        }
    }
}
