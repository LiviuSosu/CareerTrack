using CareerTrack.Application.Paging;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using Microsoft.EntityFrameworkCore;
using System;

namespace CareerTrack.Application.Tests.Articles.Query
{
    public class ArticlesTest
    {
        protected CareerTrackDbContext db;
        protected CareerTrackDbContext dbReader;
        protected DbContextOptions<CareerTrackDbContext> options;
        public PagingModel pagingModel;
        protected Guid articleIdForTheSecondArticle;
        protected string articleTitleForTheSecondArticle = "Article 2";

        public ArticlesTest()
        {
            articleIdForTheSecondArticle = Guid.Parse("8FD637BF-53E6-41B9-7E42-08D690385B3B");
            options = new DbContextOptionsBuilder<CareerTrackDbContext>()
                        .UseInMemoryDatabase(databaseName: "CareerTrackArticles").Options;

            pagingModel = new PagingModel
            {
                Field = "Username"
            };
            var order = new Order();
            pagingModel.Order = order;
            pagingModel.PageNumber = 1;
            pagingModel.PageSize = 2;
        }

        protected CareerTrackDbContext InitializeDatabase(string databaseName)
        {
            var options = new DbContextOptionsBuilder<CareerTrackDbContext>()
                            .UseInMemoryDatabase(databaseName: databaseName).Options;

            db = new CareerTrackDbContext(options);

            db.Articles.RemoveRange(db.Articles);
            db.SaveChanges();
            db.Articles.AddRange(new[] {
                new Article {
                    Title = "Article 1",
                    Link = "www.link1.com"
                    },
                new Article {
                    Id = articleIdForTheSecondArticle,
                    Title = articleTitleForTheSecondArticle,
                    Link = "www.link2.com"
                    },
                new Article {
                    Title = "Article 3",
                    Link = "www.link3.com"
                    }
                });


            db.SaveChanges();

            return db;
        }
    }
}
