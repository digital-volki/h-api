using HotChocolate.Types.Relay;
using Leifez.Application.Domain.Models;
using Leifez.General;
using System.Collections.Generic;
using System.Linq;

namespace Leifez.Images.Inputs
{
    public record CreateImagesInput(
        IEnumerable<string> Base64Images,
        [ID(nameof(Collection))] string CollectionId) : IInput
    {
        public bool Validate()
        {
            return this != null 
                && Base64Images != null
                && Base64Images.Any();
        }
    }
}
