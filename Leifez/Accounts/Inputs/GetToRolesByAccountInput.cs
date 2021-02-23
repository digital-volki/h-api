using Leifez.General;

namespace Leifez.Accounts.Inputs
{
    public record GetToRolesByAccountInput(
    string AccountId) : IInput
    {
        public bool Validate()
        {
            return this != null && !string.IsNullOrEmpty(AccountId);
        }
    }
}
