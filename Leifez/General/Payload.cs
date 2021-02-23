using Leifez.Core.Infrastructure.Exceptions;
using System.Collections.Generic;

namespace Leifez.General
{
    public abstract class Payload
    {
#nullable enable
        protected Payload(IReadOnlyList<UserError>? errors = null)
        {
            Errors = errors;
        }

        public IReadOnlyList<UserError>? Errors { get; }
#nullable enable
    }
}
