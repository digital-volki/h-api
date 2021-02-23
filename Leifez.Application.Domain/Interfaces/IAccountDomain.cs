using Leifez.Application.Domain.Models;
using Leifez.Core.PostgreSQL.Models;
using System.Collections.Generic;

namespace Leifez.Application.Domain.Interfaces
{
    public interface IAccountDomain
    {
        DbIdentityUser GetAccountByEmail(string userName);
        Account GetAccountById(string accountId);
        Account CreateAccount(DbIdentityUser account, bool isFindingExist = false);
        IEnumerable<DbIdentityRole> GetRolesByAccountId(string accountId);
        bool AddRolesToAccount(string accountId, IEnumerable<DbIdentityRole> roles);
        bool AddRole(DbIdentityRole role);
        DbIdentityRole GetRoleByName(string nameRole);
        IEnumerable<DbIdentityRole> GetRolesByNames(IEnumerable<string> nameRoles);
    }
}
