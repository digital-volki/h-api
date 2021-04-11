using Leifez.Core.PostgreSQL.Models.Enums;
using Leifez.General;
using System.Collections.Generic;

namespace Leifez.Collections.Inputs
{
    public record AddCollectionYourSelfInput(
        string CollectionId) : IInput
    {
        public bool Validate()
        {
            return this != null
                && !string.IsNullOrEmpty(CollectionId);
        }
    };
}
