using System.Runtime.Serialization;

namespace Leifez.Domain.Account.Models
{
    [DataContract]
    public class AccountModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}
