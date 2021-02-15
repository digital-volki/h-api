using Leifez.DataAccess.PostgreSQL.Models.Enums;

namespace Leifez.DataAccess.PostgreSQL.Models
{
    public class DbTag
    {
        public int TagId { get; set; }
        public string Title { get; set; }
        public TagsType Type { get; set; }
        public int Quantity { get; set; }
        public bool Danger { get; set; }
    }
}
