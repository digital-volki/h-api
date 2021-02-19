using Leifez.Application.Domain.Models;

namespace Leifez.Application.Domain.Interfaces
{
    public interface ICollectionDomain
    {
        Collection GetCollection(int collectionId);
    }
}
