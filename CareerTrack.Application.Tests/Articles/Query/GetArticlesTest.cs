﻿using CareerTrack.Application.Paging;
using CareerTrack.Domain.Entities;
using CareerTrack.Persistance;
using Microsoft.EntityFrameworkCore;
using System;

namespace CareerTrack.Application.Tests.Articles.Query
{
    public class GetArticlesTest
    {
        public CareerTrackDbContext db;
        public PagingModel pagingModel;
        protected Guid articleIdForTheSecondArticle;
        protected string articleTitleForTheSecondArticle;

        public GetArticlesTest()
        {
            articleIdForTheSecondArticle = Guid.Parse("8FD637BF-53E6-41B9-7E42-08D690385B3B");
            articleTitleForTheSecondArticle = "Article 2";

            var options = new DbContextOptionsBuilder<CareerTrackDbContext>().
              UseInMemoryDatabase(databaseName: "ReadCareerTrackUsers").Options;

            db = new CareerTrackDbContext(options);

            db.Articles.AddRange(new[] {
                new Article {
                    Id = Guid.Parse("8464B045-6F16-4A73-7E41-08D690385B3B"),
                    Title = "Article 1",
                    Link = "www.link1.com"
                    },
                new Article {
                    Id = articleIdForTheSecondArticle,
                    Title = articleTitleForTheSecondArticle,
                    Link = "www.link2.com"
                    },
                new Article {
                    Id = Guid.Parse("FEA44EA2-1D4C-49BB-92A0-1AD6899CA220"),
                    Title = "Article 3",
                    Link = "www.link3.com"
                    }
                });

            try
            {
                db.SaveChanges();
            }
            catch(ArgumentException )
            {
                //in case it was added the same Id
            }

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