using System;
using System.Collections.Generic;

namespace Leifez.Application.Domain.Models
{
    public class Image
    {
        public string Guid { get; set; }
        public string Hash { get; set; }
        public string Data { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
