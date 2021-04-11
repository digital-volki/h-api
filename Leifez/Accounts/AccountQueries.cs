using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using Leifez.Accounts.Inputs;
using Leifez.Application.Domain.Models;
using Leifez.Application.Service.Interfaces;
using Leifez.Common.Configuration;
using Leifez.Common.Web.Auth;
using Leifez.Common.Web.Auth.Models;
using Leifez.Core.Infrastructure.Exceptions;
using Leifez.General;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Leifez.Accounts
{
    [ExtendObjectType(Name = "Query")]
    public class AccountQueries
    {
        public PayloadBase<string> Authorization(
            [Service] LeifezAuthenticator authenticator,
            AuthorizationInput input)
        {
            var errors = new List<UserError>();

            if (!input.Validate())
            {
                var error = new UserError
                (
                    message: "Input empty or incomplete.",
                    code: "400"
                );
                errors.Add(error);
                return new PayloadBase<string>(errors);
            }

            var tokenRequest = new TokenRequest
            {
                Login = input.Login,
                Password = input.Password,
                GrantType = input.GrantType
            };

            var token = input.GrantType switch
            {
                GrantType.Password => authenticator.AuthByCredentials(tokenRequest),
                _ => throw new NotImplementedException()
            };

            if (string.IsNullOrEmpty(token))
            {
                var error = new UserError
                (
                    message: "Authorization denied.",
                    code: "403"
                );
                errors.Add(error);
                return new PayloadBase<string>(errors);
            }

            return new PayloadBase<string>(token);
        }

        [Authorize(Roles = new[] { "Admin" })]
        public PayloadBase<IEnumerable<string>> GetRolesByAccount(
            [Service] IAccountService accountService,
            GetToRolesByAccountInput input)
        {
            var errors = new List<UserError>();

            if (!input.Validate())
            {
                var error = new UserError
                (
                    message: "Input empty or incomplete.",
                    code: "400"
                );
                errors.Add(error);
                return new PayloadBase<IEnumerable<string>>(errors);
            }

            var roles = accountService.GetRolesByAccountId(input.AccountId);

            if (roles == null || roles.Count() <= 0)
            {
                var error = new UserError
                (
                    message: "Account not exitst.",
                    code: "404"
                );
                errors.Add(error);
                return new PayloadBase<IEnumerable<string>>(errors);
            }

            return new PayloadBase<IEnumerable<string>>(roles.Select(r => r.Value));
        }

        [Authorize]
        public PayloadBase<Account> GetAccount(
            [Service] IAccountService accountService,
            [CurrentUserGlobalState] CurrentUser currentUser,
            GetAccountInput input)
        {
            var errors = new List<UserError>();

            if (!input.Validate())
            {
                var error = new UserError
                (
                    message: "Input empty or incomplete.",
                    code: "400"
                );
                errors.Add(error);
                return new PayloadBase<Account>(errors);
            }

            Account account = accountService.GetAccount(
                string.IsNullOrEmpty(input.UserId) 
                    ? currentUser.AccountId.ToString() 
                    : input.UserId);

            if (account == null)
            {
                var error = new UserError
                (
                    message: "Account not found.",
                    code: "404"
                );
                errors.Add(error);
                return new PayloadBase<Account>(errors);
            }

            return new PayloadBase<Account>(account);
        }

        public string GetEnvironment()
        {
            return AppConfiguration.EnvironmentName;
        }
    }
}
