using MediatR;
using System;

namespace CareerTrack.Application.Handlers.Articles.Commands.Update
{
    public class UpdateArticleCommand : ArticleBaseModel, IRequest
    {
        public Guid Id { get; set; }
    }
}
