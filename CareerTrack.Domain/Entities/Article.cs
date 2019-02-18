using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CareerTrack.Domain.Entities
{
    public class Article
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }
    }
}
