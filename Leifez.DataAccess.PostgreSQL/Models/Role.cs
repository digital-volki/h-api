using System.ComponentModel.DataAnnotations;

namespace Leifez.DataAccess.PostgreSQL.Models
{
    public class Role
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
    }
}
