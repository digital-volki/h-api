using Leifez.General;

namespace Leifez.Accounts.Inputs
{
    public record CreateAccountInput(
        string UserName,
        string Email,
        string Password) : IInput
    {
        public bool Validate()
        {
            return this != null
                && !string.IsNullOrEmpty(Email)
                && !string.IsNullOrEmpty(UserName)
                && !string.IsNullOrEmpty(Password);
        }
    };
}
