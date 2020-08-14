using AutoMapper;
using CareerTrack.Application.Handlers.Articles.Commands.Create;
using CareerTrack.Domain.Entities;

namespace CareerTrack.Application.Handlers.Articles.Queries.GetArticles
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
