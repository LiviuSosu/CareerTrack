using AutoMapper;
using CareerTrack.Application.Handlers.Articles.Commands.Create;
using CareerTrack.Application.Handlers.Articles.Commands.Delete;
using CareerTrack.Application.Handlers.Articles.Commands.Update;
using CareerTrack.Domain.Entities;

namespace CareerTrack.Application.Handlers.Articles.Queries.GetArticles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleLookupModel>();
            CreateMap<CreateArticleCommand, Article>();
            CreateMap<UpdateArticleCommand, Article>();
            CreateMap<DeleteArticleCommand, Article>();
        }
    }
}
