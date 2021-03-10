using Leifez.General;

namespace Leifez.Images.Inputs
{
    public record CreateImageInput(
        string ContentImage) : IInput
    {
        public bool Validate()
        {
            return this != null && !string.IsNullOrEmpty(ContentImage);
        }
    }
}
