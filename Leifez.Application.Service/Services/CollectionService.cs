using AutoMapper;
using Leifez.Application.Domain.Interfaces;
using Leifez.Application.Domain.Models;
using Leifez.Application.Service.Interfaces;
using Leifez.Common;
using Leifez.Common.Mapping;
using Leifez.Core.Infrastructure.Exceptions;
using Leifez.Core.PostgreSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Leifez.Application.Service.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionDomain _collectionDomain;
        private readonly IAccountDomain _accountDomain;
        private readonly IImageDomain _imageDomain;
        private readonly ITagDomain _tagDomain;
        private readonly IMapper _mapper;

        public CollectionService(
            ICollectionDomain collectionDomain,
            IAccountDomain accountDomain,
            IImageDomain imageDomain,
            ITagDomain tagDomain,
            IMapper mapper)
        {
            _collectionDomain = collectionDomain;
            _accountDomain = accountDomain;
            _imageDomain = imageDomain;
            _tagDomain = tagDomain;
            _mapper = mapper;
        }

        public int Create(Collection collection)
        {
            DbCollection dbCollection = collection.Map<DbCollection>(_mapper);
            dbCollection.Author = _accountDomain.GetAccount(collection.AuthorId);
            dbCollection.Tags = _tagDomain.Get(collection.Tags).ToList();
            dbCollection.CreatedAt = DateTime.UtcNow;
            dbCollection.UpdatedAt = DateTime.UtcNow;

            return _collectionDomain.Create(dbCollection);
        }

        public Collection GetCollection(int collectionId)
        {
            var collection = _collectionDomain.GetCollection(collectionId).Map<Collection>(_mapper);
            if (collection == null)
            {
                throw new QueryException
                (
                    message: $"Collection by {collectionId} not found.",
                    code: "404"
                );
            }
            return collection;
        }

        public IQueryable<Collection> GetCollections()
        {
            var collections = _collectionDomain.GetCollections();
            if (collections == null)
            {
                throw new QueryException
                (
                    message: "No collections found.",
                    code: "404"
                );
            }
            return collections;
        }

        public bool Update(Collection collection)
        {
            DbCollection dbCollection = _collectionDomain.GetCollection(collection.Id);
            dbCollection.Title = string.IsNullOrEmpty(collection.Title) ? dbCollection.Title : collection.Title;
            dbCollection.Description = string.IsNullOrEmpty(collection.Description) ? dbCollection.Description : collection.Description;

            if (collection.Tags != null)
            {
                List<DbTag> tags = _tagDomain.Get(collection.Tags).Concat(dbCollection.Tags).DistinctBy(t => t.Id).ToList();
                dbCollection.Tags = tags;
            }

            if (collection.Images != null)
            {
                List<DbImage> images = _imageDomain.Get(collection.Images).Concat(dbCollection.Images).DistinctBy(i => i.Guid).ToList();
                dbCollection.Images = images;
            }

            dbCollection.UpdatedAt = DateTime.UtcNow;

            return _collectionDomain.Update(dbCollection);
        }
    }
}
