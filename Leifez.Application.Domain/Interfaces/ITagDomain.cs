using Leifez.Application.Domain.Models;
using Leifez.Core.PostgreSQL.Models;
using System.Collections.Generic;

namespace Leifez.Application.Domain.Interfaces
{
    public interface ITagDomain
    {
        IEnumerable<Tag> GetAll();
        IEnumerable<DbTag> Get(IEnumerable<int> ids);
        DbTag Get(int id);
        int GetQuantityImages(int tagId);
    }
}
