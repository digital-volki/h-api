using Leifez.General;

namespace Leifez.Images.Inputs
{
    public record GetImageInput(
        string Guid) : IInput
    {
        public bool Validate()
        {
            return this != null && !string.IsNullOrEmpty(Guid);
        }
    }
}
