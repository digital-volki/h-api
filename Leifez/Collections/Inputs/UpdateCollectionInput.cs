using Leifez.General;
using System;
using System.Collections.Generic;

namespace Leifez.Collections.Inputs
{
    public record UpdateCollectionInput(
        string Id,
        string Title = "",
        string Description = "",
        IEnumerable<int> Tags = default,
        IEnumerable<string> Images = default) : IInput
    {
        public bool Validate()
        {
            return this != null
                && Id != Guid.Empty.ToString();
        }
    };
}
