using HotChocolate.Types.Relay;
using Leifez.Application.Domain.Models;
using Leifez.General;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Leifez.Images.Inputs
{
    public record ChangeImageInput(
        string imageGuid,
        [ID(nameof(Tag))] IEnumerable<int> Tags) : IInput
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
