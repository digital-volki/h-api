namespace Leifez.Common.Web.Auth.Models
{
    public class TokenRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        //public string RefreshToken { get; set; }
        public GrantType GrantType { get; set; }
    }
}
