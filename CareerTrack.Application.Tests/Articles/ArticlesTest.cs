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
        protected DbContextOptions<CareerTrackDbContext> options;
        public PagingModel pagingModel;
        //protected Guid articleIdForTheFirstArticle;
        //protected Guid articleIdForTheSecondArticle;
        //protected string articleTitleForTheSecondArticle;
        protected Guid articleIdForTheThirdArticle;

        public ArticlesTest()
        {
            //articleIdForTheFirstArticle = Guid.Parse("8464B045-6F16-4A73-7E41-08D690385B3B");
            //articleIdForTheSecondArticle = Guid.Parse("8FD637BF-53E6-41B9-7E42-08D690385B3B");
            //articleTitleForTheSecondArticle = "Article 2";
            options = new DbContextOptionsBuilder<CareerTrackDbContext>()
                        .UseInMemoryDatabase(databaseName: "CareerTrackArticles").Options;

            articleIdForTheThirdArticle = Guid.Parse("FEA44EA2-1D4C-49BB-92A0-1AD6899CA220");


            pagingModel = new PagingModel
            {
                Field = "Username"
            };
            var order = new Order();
            pagingModel.Order = order;
            pagingModel.PageNumber = 1;
            pagingModel.PageSize = 2;
        }

        protected void InitializeDatabase()
        {
            var options = new DbContextOptionsBuilder<CareerTrackDbContext>()
                            .UseInMemoryDatabase(databaseName: "CareerTrackArticles").Options;

            db = new CareerTrackDbContext(options);

            db.Articles.RemoveRange(db.Articles);
            db.SaveChanges();
            db.Articles.AddRange(new[] {
                new Article {
                    //Id = articleIdForTheFirstArticle,
                    Title = "Article 1",
                    Link = "www.link1.com"
                    },
                new Article {
                    //Id = articleIdForTheSecondArticle,
                    Title = "Article 2",
                    Link = "www.link2.com"
                    },
                new Article {
                    Id = articleIdForTheThirdArticle,
                    Title = "Article 3",
                    Link = "www.link3.com"
                    }
                });


            db.SaveChanges();
        }
    }
}
