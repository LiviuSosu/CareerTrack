using MediatR;
using System;

namespace CareerTrack.Application.Articles
{
    public class CreateArticleCommand : IRequest
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
    }
}
