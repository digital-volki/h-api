using HotChocolate;
using HotChocolate.Types;
using Leifez.Application.Domain.Models;
using Leifez.Application.Service.Interfaces;
using Leifez.AppStart;
using Leifez.DataAccess.Interfaces;
using Leifez.DataAccess.PostgreSQL.Models;
using Leifez.DataAccess.PostgreSQL.Models.Enums;
using Leifez.GraphQL.Enums;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace Leifez.GraphQL.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class CollectionQuery//: DataAccess.PostgreSQL.ICollectionService
    {
        //private readonly ICollectionService _collectionService;

        //public CollectionQuery()
        //{
        //    _collectionService = LeifezUnityConfig.GetConfiguredContainer().Resolve<ICollectionService>();
        //}

        //public Collection GetCollectionTest(int collectionId)
        //{
        //    return _collectionService.GetCollectionTest(collectionId); 
        //}

        //public Collection GetCollectionDb(int collectionId)
        //{
        //    var dbModel = LeifezUnityConfig.GetConfiguredContainer().Resolve<IDataContext>().GetQueryable<DbCollection>().Where(c => c.CollectionId == collectionId).FirstOrDefault();
        //    //var dbModel = _dataContext.GetQueryable<DbCollection>().Where(c => c.CollectionId == collectionId).FirstOrDefault();
        //    Collection collection = new Collection()
        //    {
        //        Id = dbModel.CollectionId,
        //        Author = dbModel.Author,
        //        Description = dbModel.Description,
        //        Title = dbModel.Title
        //    };
        //    return collection;
        //}

        public Collection GetCollection(int collectionId)
        {
            return new Collection()
            {
                Id = collectionId,
                Author = "Leifez",
                Title = $"Hentai #{collectionId}",
                Description = $"Collection from news by Leifez",
                Tags = new List<Tag>()
                {
                    new Tag()
                    {
                         Id = 1,
                         Danger = false,
                         Name = "YaoYao",
                         Quantity = 74,
                         Type = TagsType.Artist
                    },
                    new Tag()
                    {
                         Id = 2,
                         Danger = false,
                         Name = "Ahegao",
                         Quantity = 1623,
                         Type = TagsType.Genre
                    },
                    new Tag()
                    {
                         Id = 3,
                         Danger = false,
                         Name = "Yuri",
                         Quantity = 1623,
                         Type = TagsType.Genre
                    },
                }
            };
        }

        //public List<Collection> GetCollectionsNews(int limit, int offset, int page, FilterType type, string search, List<Tag> tags)
        //{
        //    List<Collection> collectionsResult = new List<Collection>();
        //    for (int i = 1; i <= limit; i++)
        //    {
        //        collectionsResult.Add(GetCollection(i + limit * page));
        //    }

        //    return collectionsResult;
        //}
    }
}
