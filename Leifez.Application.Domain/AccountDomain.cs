using AutoMapper;
using Leifez.Application.Domain.Interfaces;
using Leifez.Application.Domain.Models;
using Leifez.Common.Mapping;
using Leifez.Core.PostgreSQL;
using Leifez.Core.PostgreSQL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Leifez.Application.Domain
{
    public class AccountDomain : IAccountDomain
    {
        private readonly IDataContext _dataContext;
        private readonly IMapper _mapper;

        public AccountDomain(
            IDataContext dataContext,
            IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public DbUser GetAccountByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }

            return _dataContext.GetQueryable<DbUser>().Where(c => c.Email == email).FirstOrDefault();
        }

        public DbUser GetAccount(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            return _dataContext.GetQueryable<DbUser>().Where(c => c.Id == id).FirstOrDefault();
        }

        public Account GetAccountById(string accountId)
        {
            if (string.IsNullOrEmpty(accountId))
            {
                return null;
            }

            return _dataContext.GetQueryable<DbUser>()
                .Include(u => u.Collections)
                .Where(u => u.Id == accountId).FirstOrDefault().Map<DbUser, Account>(_mapper);
        }

        /// <summary>
        /// Создаёт новый Аккаунт
        /// </summary>
        /// <param name="account"></param>
        /// <param name="isFindingExist">Была ли до вызова проверка на существование Аккаунта с таким Email</param>
        /// <returns></returns>
        public Account CreateAccount(DbUser account, bool isFindingExist = false)
        {
            if (account == null)
            {
                return null;
            }

            if (!isFindingExist)
            {
                var existAccount = GetAccountByEmail(account.Email);

                if (existAccount != null)
                {
                    return null;
                }
            }

            var dbAccount = _dataContext.Insert(account);

            if (dbAccount == null)
            {
                return null;
            }

            if (_dataContext.Save() == 0)
            {
                return null;
            }

            return dbAccount.Map<DbUser, Account>(_mapper);
        }

        public IEnumerable<DbRole> GetRolesByAccountId(string accountId)
        {
            if (string.IsNullOrEmpty(accountId))
            {
                return null;
            }

            var roleIds = _dataContext.GetQueryable<IdentityUserRole<string>>().Where(a => a.UserId == accountId).Select(a => a.RoleId).ToList();
            return _dataContext.GetQueryable<DbRole>().Where(r => roleIds.Contains(r.Id));
        }

        public bool AddRolesToAccount(string accountId, IEnumerable<DbRole> roles)
        {
            if (string.IsNullOrEmpty(accountId) || roles == null || roles.Count() <= 0)
            {
                return false;
            }

            var currentRoles = GetRolesByAccountId(accountId).ToList();
            roles = roles.Where(r => !(currentRoles.Select(a => a.Name).Contains(r.Name)));
             
            var userRoles = roles.Select(r => new IdentityUserRole<string>() { UserId = accountId, RoleId = r.Id });

            if (userRoles.Count() <= 0)
            {
                return true;
            }

            var insertUserRoles = _dataContext.InsertMany(userRoles);

            if (insertUserRoles == null)
            {
                return false;
            }

            return _dataContext.Save() != 0;
        }

        public bool AddRole(DbRole role)
        {
            if (role == null)
            {
                return false;
            }

            var insertRole = _dataContext.Insert(role);
            
            if (insertRole == null)
            {
                return false;
            }

            return _dataContext.Save() != 0;
        }

        public DbRole GetRoleByName(string nameRole)
        {
            if (string.IsNullOrEmpty(nameRole))
            {
                return null;
            }

            return _dataContext.GetQueryable<DbRole>().Where(r => r.Name == nameRole).FirstOrDefault();
        }

        public IEnumerable<DbRole> GetRolesByNames(IEnumerable<string> nameRoles)
        {
            if (nameRoles == null || nameRoles.Count() <= 0)
            {
                return null;
            }

            return _dataContext.GetQueryable<DbRole>().Where(r => nameRoles.Contains(r.Name));
        }
    }
}
