using HotChocolate.Types.Relay;
using Leifez.Application.Domain.Models;
using Leifez.Core.PostgreSQL.Models.Enums;
using Leifez.General;
using System.Collections.Generic;

namespace Leifez.Collections.Inputs
{
    public record CreateCollectionInput(
        string Title,
        string Description,
        [ID(nameof(Tag))] IEnumerable<int> Tags,
        ContentType contentType,
        bool IsAdult,
        PermissionType permissionType) : IInput
    {
        public bool Validate()
        {
            return this != null
                && !string.IsNullOrEmpty(Title)
                && Description != null
                && Tags != null;
        }
    };
}
