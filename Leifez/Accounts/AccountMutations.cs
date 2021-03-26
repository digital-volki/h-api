using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using Leifez.Accounts.Inputs;
using Leifez.Application.Domain.Models;
using Leifez.Application.Service.Interfaces;
using Leifez.Common.Web.Auth;
using Leifez.Common.Web.Auth.Models;
using Leifez.Core.Infrastructure.Exceptions;
using Leifez.General;
using System.Collections.Generic;

namespace Leifez.Accounts
{
    [ExtendObjectType(Name = "Mutation")]
    public class AccountMutations
    {
        public PayloadBase<string> CreateAccount(
            [Service] IAccountService accountService,
            [Service] LeifezAuthenticator authenticator,
            CreateAccountInput input)
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

            if (accountService.GetAccountByEmail(input.Email) != null)
            {
                var error = new UserError
                (
                    message: "An account with this Email already exists.",
                    code: "409"
                );
                errors.Add(error);
                return new PayloadBase<string>(errors);
            }

            var account = new Account
            {
                UserName = input.UserName,
                Email = input.Email
            };

            account = accountService.RegisterAccount(account, input.Password, true);

            if (account == null)
            {
                var error = new UserError
                (
                    message: "Failed to create account.",
                    code: "500"
                );
                errors.Add(error);
                return new PayloadBase<string>(errors);
            }

            var token = authenticator.AuthByCredentials
                (
                    new TokenRequest
                    {
                        Login = input.Email,
                        Password = input.Password,
                        GrantType = GrantType.Password
                    }
                );

            return new PayloadBase<string>(token);
        }

        [Authorize(Roles = new[] { "Admin" })]
        public PayloadBase<bool> AddRoleToAccount(
            [Service] IAccountService accountService,
            AddRoleToAccountInput input)
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
                return new PayloadBase<bool>(errors);
            }

            var result = accountService.AddRolesToAccount(input.AccountId, input.Roles);

            if (!result)
            {
                var error = new UserError
                (
                    message: "Failed to add roles to account.",
                    code: "500"
                );
                errors.Add(error);
                return new PayloadBase<bool>(errors);
            }

            return new PayloadBase<bool>(result);
        }

        [Authorize(Roles = new[] { "Admin" })]
        public PayloadBase<bool> AddRole(
            [Service] IAccountService accountService,
            AddRoleInput input)
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
                return new PayloadBase<bool>(errors);
            }

            var result = accountService.AddRole(input.Name);

            if (!result)
            {
                var error = new UserError
                (
                    message: "Failed to add role.",
                    code: "500"
                );
                errors.Add(error);
                return new PayloadBase<bool>(errors);
            }

            return new PayloadBase<bool>(result);
        }
    }
}
