using System;

namespace CareerTrack.Application.Articles.Queries.GetArticles
{
    public class ArticleLookupModel
    {
        public Guid Id { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
    }
}
