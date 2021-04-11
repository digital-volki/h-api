using System;
using System.Collections.Generic;

namespace Leifez.Core.PostgreSQL.Models
{
    public class DbCollection
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DbUser Author { get; set; }
        public ICollection<DbTag> Tags { get; set; } = new List<DbTag>();
        public ICollection<DbImage> Images { get; set; } = new List<DbImage>();
    }
}
