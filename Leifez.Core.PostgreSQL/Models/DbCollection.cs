using System.Collections.Generic;

namespace Leifez.Core.PostgreSQL.Models
{
    public class DbCollection
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        //public ICollection<DbTag> Tags { get; set; }
    }
}
