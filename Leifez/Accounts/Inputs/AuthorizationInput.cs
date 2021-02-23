using Leifez.Common.Web.Auth.Models;
using Leifez.General;

namespace Leifez.Accounts.Inputs
{
    public record AuthorizationInput(
    string Login,
    string Password,
    GrantType GrantType) : IInput
    {
        public bool Validate()
        {
            return this != null
                && !string.IsNullOrEmpty(Login)
                && !string.IsNullOrEmpty(Password);
        }
    };
}
