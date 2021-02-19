using AutoMapper;
using Leifez.Application.Domain.Interfaces;
using Leifez.Application.Domain.Models;
using Leifez.Application.Service.Interfaces;

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
        public Collection GetCollectionTest(int collectionId)
        {
            return _collectionDomain.GetCollection(collectionId);
        }
    }
}
