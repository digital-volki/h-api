using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace Leifez.Core.BearerAuth
{
    public static class LeifezAuthenticator
    {
        public static IPrincipal Authenticate(string token)
        {
            var identity = ParseJwtToken(token);
            return identity == null ? null : new ClaimsPrincipal(identity);
        }

        private static IIdentity ParseJwtToken(string token)
        {
            var claims = ParseClaimsFromToken(token);
            if (claims == null)
            {
                return null;
            }
            return IsExpiredJwt(claims) ? null : GetIdentity(claims);
        }

        private static UserContextTokenClaims ParseClaimsFromToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            var jwtParts = token.Split('.');
            if (jwtParts.Length != 3 || jwtParts.Any(string.IsNullOrWhiteSpace))
            {
                return null;
            }

            if (!IsValidJwtHeader(jwtParts[0]))
            {
                return null;
            }

            if (!IsValidSignature(jwtParts[0], jwtParts[1], jwtParts[2]))
            {
                return null;
            }

            return ParsePayload(jwtParts[1]);
        }

        private static bool IsValidJwtHeader(string header)
        {
            var headerJson = Encoding.UTF8.GetString(Convert.FromBase64String(header));
            return headerJson == "{\"alg\":\"HS256\",\"typ\":\"JWT\"}";
        }

        private static bool IsValidSignature(string header, string payload, string signature)
        {
            var secret = Convert.ToBase64String(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings.Get("jwt-secret")));
            var expectedSignature = CalculateSignature($"{header}.{payload}", secret);
            return signature == expectedSignature;
        }

        private static UserContextTokenClaims ParsePayload(string payload)
        {
            try
            {
                var payloadJson = Encoding.UTF8.GetString(Convert.FromBase64String(payload));
                return JsonConvert.DeserializeObject<UserContextTokenClaims>(payloadJson);
            }
            catch
            {
                return null;
            }
        }

        private static string CalculateSignature(string content, string secret)
        {
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(content));
            return Convert.ToBase64String(hash);
        }

        private static IIdentity GetIdentity(UserContextTokenClaims claims)
        {
            try
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim("AccountId", claims.AccountId),
                    new Claim("Login", claims.Login)
                }, "Bearer");
                return identity;
            }
            catch
            {
                return null;
            }
        }

        private static bool IsExpiredJwt(UserContextTokenClaims claims)
        {
            var currentTimestamp = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
            return currentTimestamp > claims.ExpiredAt;
        }
    }
}
