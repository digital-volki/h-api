using Leifez.Application.Domain.Models;
using Leifez.Core.PostgreSQL.Models;
using System.Linq;

namespace Leifez.Application.Domain.Interfaces
{
    public interface ICollectionDomain
    {
        DbCollection GetCollection(int collectionId);
        IQueryable<Collection> GetCollections();
        int Create(DbCollection collection);
        bool Update(DbCollection collection);
    }
}
