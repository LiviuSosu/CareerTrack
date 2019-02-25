using CareerTrack.Application.Articles;
using CareerTrack.Application.Articles.Commands;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Articles.Command
{
    public class CreateArticleCommandHandlerTests
    {
        private CareerTrackDbContext db;
        private Mock<IServiceProvider> serviceProviderMock;
        public CreateArticleCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<CareerTrackDbContext>()
              .UseInMemoryDatabase(databaseName: "CreateCareerTrackArticle")
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

            serviceProviderMock = new Mock<IServiceProvider>();

            serviceProviderMock.Setup(x => x.GetService(typeof(CareerTrackDbContext)))
            .Returns(db);

            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProviderMock.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory.Setup(x => x.CreateScope())
                .Returns(serviceScope.Object);
        }

        [Fact]
        public async Task CreateArticleTest()
        {
            var sut = new CreateArticleCommandHandler(db, null);

            var result = await sut.Handle(new CreateArticleCommand
            {
                Name = "Article 4",
                Link = "www.link4.com",
                ServiceProvider = serviceProviderMock.Object
            },
                    CancellationToken.None);

            result.ShouldBeOfType<Unit>();
            db.Articles.Count().ShouldBe(4);
        }
    }
}
