using Leifez.Core.PostgreSQL.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Leifez.Core.PostgreSQL.Models
{
    public class DbLike
    {
        [Key]
        public string HashId { get; set; }
        public string UserId { get; set; }
        public string EntityId { get; set; }
        public ContentType ContentType { get; set; }
    }
}
