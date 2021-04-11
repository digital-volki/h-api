using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Leifez.Application.Domain.Models;
using Leifez.Application.Service.Interfaces;
using System.Linq;

namespace Leifez.Collections
{
    [ExtendObjectType(Name = "Query")]
    public class CollectionQueries
    {
        [UsePaging]
        public IQueryable<Collection> GetCollections(
            [Service] ICollectionService collectionService,
            string userId) =>
                string.IsNullOrEmpty(userId) 
                    ? collectionService.GetCollections() 
                    : collectionService.GetCollectionsByUser(userId);

        public Collection GetCollectionById(
            [ID(nameof(Collection))] string id,
            [Service] ICollectionService collectionService) =>
                collectionService.GetCollection(id);
    }
}

// TODO: сделать проверку GUID