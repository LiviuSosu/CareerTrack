using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerTrack.Application.Tests.Articles
{
    public class ArticlesTest
    {
        public CareerTrackDbContext db;
        protected Guid firstArticleId = Guid.Parse("777ACF7F-DAF6-4DF2-B80B-F0A69248249A");
        protected Guid secondArticleId = Guid.Parse("BB13D499-FE99-4451-B632-04D7759BD6D4");
        protected Guid nonExistingArticleId = Guid.Parse("FEA44EA2-1D4C-49AC-92A0-1AD6899CA220");

        protected DbContextOptions<CareerTrackDbContext> options;

        public ArticlesTest()
        {
            options = new DbContextOptionsBuilder<CareerTrackDbContext>().
            UseInMemoryDatabase(databaseName: "CareerTrackArticles").Options;

            db = new CareerTrackDbContext(options);

            var article1 = new Article
            {
                Id = firstArticleId,
                Title = "Article 1",
                Source = "Source 1",
                Link = "www.link1.com"
            };

            var article2 = new Article
            {
                Id = secondArticleId,
                Title = "Article 2",
                Source = "Source 2",
                Link = "www.link2.com"
            };

            try
            {
                db.Articles.Add(article1);
                db.Articles.Add(article2);

                db.SaveChanges();
            }
            catch (ArgumentException)
            {
                db.Articles.RemoveRange(db.Articles);
            }
        }
    }
}
