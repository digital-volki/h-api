using Leifez.General;
using System.Collections.Generic;
using System.Linq;

namespace Leifez.Images.Inputs
{
    public record GetImagesInput(
        IEnumerable<string> Guids) : IInput
    {
        public bool Validate()
        {
            return this != null 
                && Guids != null 
                && Guids.Any();
        }
    }
}
