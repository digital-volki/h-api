using HotChocolate.Types.Relay;
using Leifez.Application.Domain.Models;
using Leifez.General;
using System;
using System.Collections.Generic;

namespace Leifez.Collections.Inputs
{
    public record UpdateCollectionInput(
        [ID(nameof(Collection))] string Id,
        string Title = "",
        string Description = "",
        [ID(nameof(Tag))] IEnumerable<int> Tags = default,
        IEnumerable<string> Images = default) : IInput
    {
        public bool Validate()
        {
            return this != null
                && Id != Guid.Empty.ToString();
        }
    };
}
