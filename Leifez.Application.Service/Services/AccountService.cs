using AutoMapper;
using Leifez.Application.Domain.Interfaces;
using Leifez.Application.Domain.Models;
using Leifez.Application.Service.Interfaces;
using Leifez.Common.Mapping;
using Leifez.Core.PostgreSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Leifez.Application.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountDomain _accountDomain;
        private readonly IMapper _mapper;
        public AccountService(
            IAccountDomain accountDomain,
            IMapper mapper)
        {
            _mapper = mapper;
            _accountDomain = accountDomain;
        }

        public Account RegisterAccount(Account account, string password, bool isFindingExist = false)
        {
            var sha256 = SHA256.Create();
            var passwordHash = Encoding.ASCII.GetString(sha256.ComputeHash(Encoding.ASCII.GetBytes(password)));

            DbIdentityUser user = account.Map<Account, DbIdentityUser>(_mapper);
            user.PasswordHash = passwordHash;
            user.Id = Guid.NewGuid().ToString();

            var result = _accountDomain.CreateAccount(user, isFindingExist);

            return result;
        }

        public Account FindAccountByEmail(string email)
        {
            return _accountDomain.GetAccountByEmail(email).Map<DbIdentityUser, Account>(_mapper);
        }

        public List<Claim> GetRolesByAccountId(string accountId)
        {
            return _accountDomain.GetRolesByAccountId(accountId).Select(r => new Claim(ClaimTypes.Role, r.Name)).ToList();
        }

        public bool AddRole(string roleName)
        {
            return _accountDomain.AddRole(new DbIdentityRole() { Name = roleName });
        }

        public bool AddRolesToAccount(string accountId, IEnumerable<string> roles)
        {
            var dbRoles = _accountDomain.GetRolesByNames(roles);
            return _accountDomain.AddRolesToAccount(accountId, dbRoles);
        }

        public bool IsAccountExist(string accountId)
        {
            return _accountDomain.GetAccountById(accountId) != null;
        }
    }
}
