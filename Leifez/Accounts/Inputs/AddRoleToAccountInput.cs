using Leifez.General;
using System.Collections.Generic;
using System.Linq;

namespace Leifez.Accounts.Inputs
{
    public record AddRoleToAccountInput(
        string AccountId,
        IEnumerable<string> Roles) : IInput
    {
        public bool Validate()
        {
            return this != null
                && !string.IsNullOrEmpty(AccountId)
                && Roles != null
                && Roles.Count() > 0;
        }
    };
}
