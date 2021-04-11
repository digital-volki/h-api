using Leifez.Application.Domain.Models;
using System.Linq;

namespace Leifez.Application.Service.Interfaces
{
    public interface ICollectionService
    {
        Collection GetCollection(string collectionId);
        IQueryable<Collection> GetCollections();
        string Create(Collection collection);
        bool Update(Collection collection);
    }
}
