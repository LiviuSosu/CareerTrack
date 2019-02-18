using MediatR;
using System;

namespace CareerTrack.Application.Articles.Commands.Delete
{
    public class DeleteArticleCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
