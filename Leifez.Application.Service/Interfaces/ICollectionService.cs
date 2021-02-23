using Leifez.Application.Domain.Models;
using System.Linq;

namespace Leifez.Application.Service.Interfaces
{
    public interface ICollectionService
    {
        Collection GetCollection(int collectionId);
        IQueryable<Collection> GetCollections();
    }
}
