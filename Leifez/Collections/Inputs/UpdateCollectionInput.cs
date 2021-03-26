using Leifez.General;
using System.Collections.Generic;
using System.Linq;

namespace Leifez.Collections.Inputs
{
    public record UpdateCollectionInput(
        int Id,
        string Title = "",
        string Description = "",
        IEnumerable<int> Tags = default,
        IEnumerable<string> Images = default) : IInput
    {
        public bool Validate()
        {
            return this != null
                && Id > 0;
        }
    };
}
