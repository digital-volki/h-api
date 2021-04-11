using Leifez.General;

namespace Leifez.Accounts.Inputs
{
    public record GetAccountInput(
        string UserId) : IInput
    {
        public bool Validate()
        {
            return this != null;
        }
    }
}
