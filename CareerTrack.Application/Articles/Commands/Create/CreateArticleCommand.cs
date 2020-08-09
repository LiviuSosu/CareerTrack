using MediatR;

namespace CareerTrack.Application.Articles.Commands.Create
{
    public class CreateArticleCommand : IRequest
    {
        public string Title { get; set; }
        public string Link { get; set; }
    }
}
