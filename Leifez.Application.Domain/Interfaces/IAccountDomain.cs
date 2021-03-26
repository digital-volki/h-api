using Leifez.Application.Domain.Models;
using Leifez.Core.PostgreSQL.Models;
using System.Collections.Generic;

namespace Leifez.Application.Domain.Interfaces
{
    public interface IAccountDomain
    {
        DbUser GetAccountByEmail(string userName);
        DbUser GetAccount(string id);
        Account GetAccountById(string accountId);
        Account CreateAccount(DbUser account, bool isFindingExist = false);
        IEnumerable<DbRole> GetRolesByAccountId(string accountId);
        bool AddRolesToAccount(string accountId, IEnumerable<DbRole> roles);
        bool AddRole(DbRole role);
        DbRole GetRoleByName(string nameRole);
        IEnumerable<DbRole> GetRolesByNames(IEnumerable<string> nameRoles);
    }
}
