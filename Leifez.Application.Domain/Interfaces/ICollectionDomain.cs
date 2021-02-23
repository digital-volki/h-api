using Leifez.Application.Domain.Models;
using System.Linq;

namespace Leifez.Application.Domain.Interfaces
{
    public interface ICollectionDomain
    {
        Collection GetCollection(int collectionId);
        IQueryable<Collection> GetCollections();
    }
}
