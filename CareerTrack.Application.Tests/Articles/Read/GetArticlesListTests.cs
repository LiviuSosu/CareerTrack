using CareerTrack.Application.Articles.Queries.GetArticleDetail;
using CareerTrack.Application.Articles.Queries.GetArticles;
using CareerTrack.Application.Paging;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CareerTrack.Application.Tests.Articles.Read
{
    public class GetArticlesListTests
    {
        public CareerTrackDbContext db;
        public PagingModel pagingModel;

        public GetArticlesListTests()
        {
            var options = new DbContextOptionsBuilder<CareerTrackDbContext>()
    .UseInMemoryDatabase(databaseName: "ReadCareerTrackUsers")
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
            pagingModel = new PagingModel
            {
                Field = "Username"
            };
            var order = new Order();
            pagingModel.Order = order;
            pagingModel.PageNumber = 1;
            pagingModel.PageSize = 2;
        }

        [Fact]
        public async Task GetFilteredArticlesTest()
        {
            var sut = new GetArticlesListQueryHandler(db);
            pagingModel.QueryFilter = "2";

            var result = await sut.Handle(new GetArticlesListQuery(pagingModel), CancellationToken.None);

            result.ShouldBeOfType<ArticlesListViewModel>();
            result.Articles.Count.ShouldBe(1);
        }
    }
}
