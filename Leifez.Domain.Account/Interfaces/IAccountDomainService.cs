using Leifez.Domain.Account.Models;

namespace Leifez.Domain.Account.Interfaces
{
    interface IAccountDomainService
    {
        AccountModel GetAccount(int accountId);
    }
}
