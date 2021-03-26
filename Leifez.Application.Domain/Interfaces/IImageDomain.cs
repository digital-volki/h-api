using Leifez.Core.PostgreSQL.Models;
using System.Collections.Generic;

namespace Leifez.Application.Domain.Interfaces
{
    public interface IImageDomain
    {
        bool Add(IEnumerable<DbImage> imageModels);
        List<DbImage> Get(IEnumerable<string> guids);
        DbImage Get(string guid);
        bool Delete(string guid);
        bool Update(DbImage dbImage);
        string FindByHash(string hash);
    }
}
