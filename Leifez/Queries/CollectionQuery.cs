using HotChocolate.Types;
using Leifez.Models;
using Leifez.Models.Enums;
using System.Collections.Generic;

namespace Leifez.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class CollectionQuery
    {
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

        public List<Collection> GetCollectionsNews(int limit, int offset, int page, FilterType type, string search, List<Tag> tags)
        {
            List<Collection> collectionsResult = new List<Collection>();
            for (int i = 1; i <= limit; i++)
            {
                collectionsResult.Add(GetCollection(i + limit * page));
            }

            return collectionsResult;
        }
    }
}
