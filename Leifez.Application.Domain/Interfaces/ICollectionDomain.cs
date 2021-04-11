using Leifez.Application.Domain.Models;
using Leifez.Core.PostgreSQL.Models;
using System.Collections.Generic;
using System.Linq;

namespace Leifez.Application.Domain.Interfaces
{
    public interface ICollectionDomain
    {
        DbCollection GetCollection(string collectionId);
        IQueryable<Collection> GetCollections();
        IQueryable<Collection> GetCollections(IEnumerable<string> collectionIds);
        DbCollection Create(DbCollection collection);
        bool Update(DbCollection collection);
    }
}
