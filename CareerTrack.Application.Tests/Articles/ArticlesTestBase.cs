using CareerTrack.Application.Paging;
using CareerTrack.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerTrack.Application.Tests.Articles
{
    public class ArticlesTestBase
    {
        protected CareerTrackDbContext db;
        protected PagingModel pagingModel;

        public ArticlesTestBase()
        {
            var options = new DbContextOptionsBuilder<CareerTrackDbContext>()
                .UseInMemoryDatabase(databaseName: "ReadCareerTrackUsers")
                .Options;

            db = new CareerTrackDbContext(options);

            //try
            //{
                //db.Articles.AddRange(new[] {
                //new Article { Id = "8464B045-6F16-4A73-7E41-08D690385B3B", UserName = "AdamCogan" },
                //new Article { Id = "8FD637BF-53E6-41B9-7E42-08D690385B3B", UserName = "JasonTaylor" },
                //new Article { Id = "FEA44EA2-1D4C-49BB-92A0-1AD6899CA220", UserName = "BrendanRichards" }
                //});

                db.SaveChanges();
            //}
            //catch (ArgumentException)
            //{
            //    //TODO: to be reviewed adding duplicates
            //    db.Database.EnsureDeleted();
            //}

            pagingModel = new PagingModel
            {
                Field = "Username"
            };
            var order = new Order();
            pagingModel.Order = order;
            pagingModel.PageNumber = 1;
            pagingModel.PageSize = 2;
        }
    }
}
