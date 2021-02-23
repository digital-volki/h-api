using Leifez.General;

namespace Leifez.Accounts.Inputs
{
    public record AddRoleInput(
        string Name) : IInput
    {
        public bool Validate()
        {
            return this != null && !string.IsNullOrEmpty(Name);
        }
    }
}
