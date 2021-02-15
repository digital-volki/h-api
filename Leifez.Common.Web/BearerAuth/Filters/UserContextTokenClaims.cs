using Newtonsoft.Json;

namespace Leifez.Core.BearerAuth
{
    public class UserContextTokenClaims
    {
        [JsonProperty("account_id")]
        public string AccountId { get; set; }
        [JsonProperty("login")]
        public string Login { get; set; }
        [JsonProperty("iat")]
        public long CreationTimestamp { get; set; }
        [JsonProperty("exp")]
        public long Ttl { get; set; }
        public long ExpiredAt => CreationTimestamp + Ttl;
    }
}
