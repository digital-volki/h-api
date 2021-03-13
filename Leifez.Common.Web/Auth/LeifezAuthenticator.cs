using AutoMapper;
using Leifez.Application.Domain.Interfaces;
using Leifez.Application.Domain.Models;
using Leifez.Application.Service.Interfaces;
using Leifez.Common.Mapping;
using Leifez.Common.Web.Auth.Models;
using Leifez.Common.Web.BearerAuth;
using Leifez.Core.PostgreSQL.Models;
using System.Security.Cryptography;
using System.Text;

namespace Leifez.Common.Web.Auth
{
    public class LeifezAuthenticator
    {
        public readonly IAccountDomain _accountDomain;
        public readonly IAccountService _accountService;
        public readonly IMapper _mapper;

        public LeifezAuthenticator(
            IAccountDomain accountDomain, 
            IAccountService accountService, 
            IMapper mapper)
        {
            _accountDomain = accountDomain;
            _accountService = accountService;
            _mapper = mapper;
        }

        public string AuthByCredentials(TokenRequest token)
        {
            return Authenticate(token.Login, token.Password).AccessToken;
        }

        private Access Authenticate(string email, string password)
        {
            var account = _accountDomain.GetAccountByEmail(email);
            var roles = _accountService.GetRolesByAccountId(account.Id);
            if (account == null)
            {
                return null;
            }

            var isValidPassword = ValidatePassword(password, account);
            if (!isValidPassword)
            {
                return null;
            }
            
            var accessToken = LeifezJwtAuthenticator.GenerateJwtToken(account.Map<DbUser, Account>(_mapper), roles);
            return new Access(accessToken);
        }

        private bool ValidatePassword(string password, DbUser account)
        {
            if (string.IsNullOrEmpty(password) || account == null)
            {
                return false;
            }
            
            var sha256 = SHA256.Create();
            var passwordHash = Encoding.ASCII.GetString(sha256.ComputeHash(Encoding.ASCII.GetBytes(password)));

            return account.PasswordHash == passwordHash;
        }
    }
}
