using System.Collections.Generic;

namespace Leifez.Application.Domain.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
