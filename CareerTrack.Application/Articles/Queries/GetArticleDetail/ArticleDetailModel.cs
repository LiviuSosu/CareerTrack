using CareerTrack.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace CareerTrack.Application.Articles.Queries.GetArticleDetail
{
    public class ArticleDetailModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }

        public static Expression<Func<Article, ArticleDetailModel>> Projection
        {
            get
            {
                return user => new ArticleDetailModel
                {
                    Id = user.Id,
                    Link = user.Link,
                    Name = user.Name
                };
            }
        }

        public static ArticleDetailModel Create(Article article)
        {
            return Projection.Compile().Invoke(article);
        }
    }
}
