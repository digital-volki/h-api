using HotChocolate.Types.Relay;
using Leifez.Application.Domain.Models;
using Leifez.Core.PostgreSQL.Models.Enums;
using Leifez.General;
using System.Collections.Generic;

namespace Leifez.Collections.Inputs
{
    public record AddCollectionYourSelfInput(
        [ID(nameof(Collection))] string CollectionId) : IInput
    {
        public bool Validate()
        {
            return this != null
                && !string.IsNullOrEmpty(CollectionId);
        }
    };
}
