using HotChocolate;
using HotChocolate.Types;
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
            string id,
            [Service] ICollectionService collectionService) =>
                collectionService.GetCollection(id);
    }
}