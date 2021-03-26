using Leifez.Application.Domain.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace Leifez.Application.Service.Interfaces
{
    public interface IAccountService
    {
        Account RegisterAccount(Account account, string password, bool isFindingExist = false);
        Account GetAccountByEmail(string email);
        Account GetAccount(string id);
        List<Claim> GetRolesByAccountId(string accountId);
        bool AddRolesToAccount(string accountId, IEnumerable<string> roles);
        bool AddRole(string roleName);
        bool IsAccountExist(string accountId);
    }
}
