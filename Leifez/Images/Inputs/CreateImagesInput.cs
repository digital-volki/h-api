using Leifez.General;
using System.Collections.Generic;
using System.Linq;

namespace Leifez.Images.Inputs
{
    public record CreateImagesInput(
        IEnumerable<string> Base64Images) : IInput
    {
        public bool Validate()
        {
            return this != null 
                && Base64Images != null
                && Base64Images.Any();
        }
    }
}
