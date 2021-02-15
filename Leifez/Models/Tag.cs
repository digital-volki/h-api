using Leifez.DataAccess.PostgreSQL.Models.Enums;

namespace Leifez.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TagsType Type { get; set; }
        public int Quantity { get; set; }
        public bool Danger { get; set; }
    }
}
