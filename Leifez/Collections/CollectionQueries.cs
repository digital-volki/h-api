using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Leifez.Application.Domain.Models;
using Leifez.Application.Service.Interfaces;
using Leifez.General;
using System.Linq;

namespace Leifez.Collections
{
    [ExtendObjectType(Name = "Query")]
    public class CollectionQueries
    {
        [UsePaging]
        public IQueryable<Collection> GetCollections(
            [Service] ICollectionService collectionService,
            [CurrentUserGlobalState] CurrentUser currentUser,
            string userId) =>
                string.IsNullOrEmpty(userId) 
                    ? collectionService.GetCollections(currentUser?.AccountId.ToString()) 
                    : collectionService.GetCollectionsByUser(userId);

        public Collection GetCollectionById(
            [ID(nameof(Collection))] string id,
            [CurrentUserGlobalState] CurrentUser currentUser,
            [Service] ICollectionService collectionService) =>
                collectionService.GetCollection(id, currentUser.AccountId.ToString());
    }
}

// TODO: сделать проверку GUID