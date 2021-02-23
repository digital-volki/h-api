using AutoMapper;
using Leifez.Application.Domain.Interfaces;
using Leifez.Application.Domain.Models;
using Leifez.Application.Service.Interfaces;
using Leifez.Core.Infrastructure.Exceptions;
using System.Linq;

namespace Leifez.Application.Service.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionDomain _collectionDomain;
        private readonly IMapper _mapper;

        public CollectionService(
            ICollectionDomain collectionDomain,
            IMapper mapper)
        {
            _collectionDomain = collectionDomain;
            _mapper = mapper;
        }
        public Collection GetCollection(int collectionId)
        {
            var collection = _collectionDomain.GetCollection(collectionId);
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
    }
}
