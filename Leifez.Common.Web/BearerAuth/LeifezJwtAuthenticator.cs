using System;
using System.Configuration;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;
using Leifez.Application.Domain.Models;

namespace Leifez.Common.Web.BearerAuth
{
    public static class LeifezJwtAuthenticator
    {
        public static string GenerateJwtToken(Account account, IEnumerable<Claim> claims)
        {
            if (claims == null || claims.Count() == 0)
            {
                return null;
            }

            var claimsList = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, account.AccountId),
                new Claim(ClaimTypes.Email, account.Email),
            };
            claimsList.AddRange(claims);

            var jwt = new JwtSecurityToken(
                issuer: ConfigurationManager.AppSettings.Get("jwt-issuer"),
                audience: ConfigurationManager.AppSettings.Get("jwt-audience"),
                notBefore: DateTime.UtcNow,
                claims: claimsList,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(int.Parse(ConfigurationManager.AppSettings.Get("jwt-ttl")))),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(
                            ConfigurationManager.AppSettings.Get("jwt-secret"))),
                    SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
