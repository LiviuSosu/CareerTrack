using System;
using System.ComponentModel.DataAnnotations;

namespace CareerTrack.Domain.Entities
{
    public class Article
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public string Source { get; set; }
    }
}
