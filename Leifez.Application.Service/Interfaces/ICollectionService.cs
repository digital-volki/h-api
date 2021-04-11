using Leifez.Application.Domain.Models;
using System.Linq;

namespace Leifez.Application.Service.Interfaces
{
    public interface ICollectionService
    {
        Collection GetCollection(string collectionId, string userId);
        IQueryable<Collection> GetCollections(string userId);
        IQueryable<Collection> GetCollectionsByUser(string userId);
        Collection Create(Collection collection);
        bool Update(Collection collection);
        bool IsBelong(string accountId);
        bool AddCollectionToUser(string collectionId, string userId);
    }
}
