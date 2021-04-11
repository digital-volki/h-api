using Leifez.Application.Domain.Models;
using System.Linq;

namespace Leifez.Application.Service.Interfaces
{
    public interface ICollectionService
    {
        Collection GetCollection(string collectionId);
        IQueryable<Collection> GetCollections();
        IQueryable<Collection> GetCollectionsByUser(string userId);
        string Create(Collection collection);
        bool Update(Collection collection);
        bool IsBelong(string accountId);
        bool AddCollectionToUser(string collectionId, string userId);
    }
}
