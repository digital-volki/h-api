using System.ComponentModel.DataAnnotations;

namespace Leifez.DataAccess.PostgreSQL.Models
{
    public class UserRole
    {
        public int Id { get; set; }

        [MaxLength(36)]
        public string UserId { get; set; }

        public int RoleId { get; set; }
    }
}
