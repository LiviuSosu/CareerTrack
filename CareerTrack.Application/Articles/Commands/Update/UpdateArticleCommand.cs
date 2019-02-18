using MediatR;
using System;

namespace CareerTrack.Application.Articles.Commands.Update
{
    public class UpdateArticleCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
    }
}
