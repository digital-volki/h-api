using System.Collections.Generic;
using System.Linq;

namespace Leifez.Application.Domain.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; }
        public string Image => Images.LastOrDefault();
        public List<int> Tags { get; set; }
        public List<string> Images { get; set; } 
    }
}
