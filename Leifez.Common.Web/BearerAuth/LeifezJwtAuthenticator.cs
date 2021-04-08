using System;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;
using Leifez.Application.Domain.Models;
using Leifez.Common.Configuration;

namespace Leifez.Common.Web.BearerAuth
{
    public static class LeifezJwtAuthenticator
    {
        public static string GenerateJwtToken(Account account, IEnumerable<Claim> claims)
        {
            var claimsList = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, account.AccountId),
                new Claim(ClaimTypes.Email, account.Email),
            };
            claimsList.AddRange(claims ?? new List<Claim>());

            var jwt = new JwtSecurityToken(
                issuer: AppConfiguration.Configuration.GetSection("JwtSettings").GetSection("jwt-issuer").Value,
                audience: AppConfiguration.Configuration.GetSection("JwtSettings").GetSection("jwt-audience").Value,
                notBefore: DateTime.UtcNow,
                claims: claimsList,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(int.Parse(AppConfiguration.Configuration.GetSection("JwtSettings").GetSection("jwt-ttl").Value))),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(
                            AppConfiguration.Configuration.GetSection("JwtSettings").GetSection("jwt-secret").Value)),
                    SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
