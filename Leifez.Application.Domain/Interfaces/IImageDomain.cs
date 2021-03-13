using Leifez.Core.PostgreSQL.Models;
using System.Collections.Generic;

namespace Leifez.Application.Domain.Interfaces
{
    public interface IImageDomain
    {
        bool Add(DbImage imageModel);
        List<DbImage> Get(IEnumerable<string> guids);
        bool Delete(string guid);
        string FindByHash(string hash);
    }
}
