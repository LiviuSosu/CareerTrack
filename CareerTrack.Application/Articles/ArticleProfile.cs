using AutoMapper;
using CareerTrack.Application.Articles.Commands.Create;
using CareerTrack.Domain.Entities;

namespace CareerTrack.Application.Articles.Queries.GetArticles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile() 
        {
            CreateMap<Article, ArticleLookupModel>();
            CreateMap<CreateArticleCommand, Article>();
        }
    }
}
