using Leifez.Core.PostgreSQL.Models.Enums;
using Leifez.General;
using System;

namespace Leifez.Common.Inputs
{
    public record LikeInput(
        string Guid,
        ContentType ContentType) : IInput
    {
        public bool Validate()
        {
            return this != null &&
                Guid != System.Guid.Empty.ToString();
        }
    }
}
