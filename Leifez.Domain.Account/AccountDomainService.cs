using Leifez.DataAccess.Interfaces;
using Leifez.DataAccess.PostgreSQL.Models;
using Leifez.Domain.Account.Interfaces;
using Leifez.Domain.Account.Models;
using System.Linq;

namespace Leifez.Domains.Account
{
    public class AccountDomainService : IAccountDomainService
    {
        private readonly IDataContext _dataContext;

        public AccountDomainService(
            IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public AccountModel GetAccount(int accountId)
        {
            var dbAccount = _dataContext.GetQueryable<DbAccount>().FirstOrDefault(a => a.AccountId == accountId);
            var accountModel = new AccountModel()
            {
                Id = dbAccount.AccountId,
                UserName = dbAccount.UserName,
                Email = dbAccount.Email,
                Password = dbAccount.Password
            };
            return accountModel;
        }
    }
}
