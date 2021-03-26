using Leifez.Core.PostgreSQL.Models.Enums;
using System.Collections.Generic;

namespace Leifez.Core.PostgreSQL.Models
{
    public class DbTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TagsType Type { get; set; }
        public bool Danger { get; set; }
        public List<DbImage> Images { get; set; } = new List<DbImage>();
        public ICollection<DbCollection> Collections { get; set; } = new List<DbCollection>();
    }
}
