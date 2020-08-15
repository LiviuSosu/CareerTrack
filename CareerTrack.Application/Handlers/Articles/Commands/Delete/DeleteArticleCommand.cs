using MediatR;
using System;

namespace CareerTrack.Application.Handlers.Articles.Commands.Delete
{
    public class DeleteArticleCommand : ArticleBaseModel, IRequest
    {
        public Guid Id { get; set; }
    }
}
