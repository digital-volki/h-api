namespace Leifez.Common.Web.Auth.Models
{
    public class Access
    {
        public string AccessToken { get; }
        public string RefreshToken { get; }

        public Access(string accessToken, string refreshToken = "")
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
