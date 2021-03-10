using Leifez.Application.Domain.Models;
using System.Collections.Generic;

namespace Leifez.Application.Domain.Interfaces
{
    public interface ITagDomain
    {
        IEnumerable<Tag> GetTags();
    }
}
