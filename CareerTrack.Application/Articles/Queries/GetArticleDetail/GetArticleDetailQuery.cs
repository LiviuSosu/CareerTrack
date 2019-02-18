using MediatR;
using System;

namespace CareerTrack.Application.Articles.Queries.GetArticleDetail
{
    public class GetArticleDetailQuery : IRequest<ArticleDetailModel>
    {
        public Guid Id { get; set; }
    }
}
