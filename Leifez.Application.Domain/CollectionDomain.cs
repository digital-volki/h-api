using AutoMapper;
using Leifez.Application.Domain.Interfaces;
using Leifez.Application.Domain.Models;
using Leifez.Common.Mapping;
using Leifez.DataAccess.Interfaces;
using Leifez.DataAccess.PostgreSQL.Models;
using System.Linq;

namespace Leifez.Application.Domain
{
    public class CollectionDomain : ICollectionDomain
    {
        private readonly IDataContext _dataContext;
        private readonly IMapper _mapper;

        public CollectionDomain(
            IDataContext dataContext,
            IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public Collection GetCollection(int collectionId)
        {
            return _dataContext.GetQueryable<DbCollection>().Where(c => c.CollectionId == collectionId).FirstOrDefault().Map<DbCollection, Collection>(_mapper);
        }
    }
}
