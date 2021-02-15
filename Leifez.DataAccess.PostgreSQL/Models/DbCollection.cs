using System.Collections.Generic;

namespace Leifez.DataAccess.PostgreSQL.Models
{
    public class DbCollection
    {
        public int CollectionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public ICollection<DbTag> Tags { get; set; }
    }
}
