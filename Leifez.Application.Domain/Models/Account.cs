using System.Collections.Generic;

namespace Leifez.Application.Domain.Models
{
    public class Account
    {
        public string AccountId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<Collection> Collections { get; set; }
    }
}
