using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CareerTrack.Domain.Entities
{
    public class Article
    {
        [Key]
        public Guid ArticleId { get; set; }

        public string ArticleName { get; set; }

        public string ArticleLink { get; set; }
    }
}
