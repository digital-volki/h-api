using Leifez.General;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Leifez.Images.Inputs
{
    public record ChangeImageInput(
        string imageGuid,
        IEnumerable<int> Tags) : IInput
    {
        public bool Validate()
        {
            return this != null
                && !string.IsNullOrEmpty(imageGuid)
                && Guid.TryParse(imageGuid, out _)
                && Tags.Any();
        }
    }
}
