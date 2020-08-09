using CareerTrack.Application.Articles.Queries.GetArticles;
using MediatR;
using System;

namespace CareerTrack.Application.Articles.Queries.GetArticle
{
    public class GetArticleQuery : IRequest<ArticleLookupModel>
    {
        public Guid ArticleId { get; set; }

        public GetArticleQuery (Guid articleId)
        {
            ArticleId = articleId;
        }
    }
}
