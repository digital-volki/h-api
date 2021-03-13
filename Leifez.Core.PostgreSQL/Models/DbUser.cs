using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Leifez.Core.PostgreSQL.Models
{
    public class DbUser : IdentityUser
    {
        public ICollection<DbCollection> Collections { get; set; } = new List<DbCollection>();
    }
}
