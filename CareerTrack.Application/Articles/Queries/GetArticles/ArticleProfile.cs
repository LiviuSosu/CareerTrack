using AutoMapper;
using CareerTrack.Domain.Entities;

namespace CareerTrack.Application.Articles.Queries.GetArticles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile() 
        {
            CreateMap<Article, ArticleLookupModel>();
        }
    }
}
