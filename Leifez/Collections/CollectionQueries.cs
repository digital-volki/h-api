using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
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
            [Service] ICollectionService collectionService) =>
            collectionService.GetCollections();

        public Collection GetCollectionById(
            int id,
            [Service] ICollectionService collectionService) =>
            collectionService.GetCollection(id);
    }
}