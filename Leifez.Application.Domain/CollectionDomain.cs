using AutoMapper;
using Leifez.Application.Domain.Interfaces;
using Leifez.Application.Domain.Models;
using Leifez.Common.Mapping;
using Leifez.Core.PostgreSQL;
using Leifez.Core.PostgreSQL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Leifez.Application.Domain
{
    public class CollectionDomain : ICollectionDomain
    {
        private readonly IDataContext _dataContext;
         readonly IMapper _mapper;

        public CollectionDomain(
            IDataContext dataContext,
            IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public string Create(DbCollection dbCollection)
        {
            string result = Guid.Empty.ToString();
            if (dbCollection == null)
            {
                return result;
            }

            var dbCollectionResult = _dataContext.Insert(dbCollection);

            if (dbCollectionResult == null)
            {
                return result;
            }

            if (_dataContext.Save() != 0)
            {
                result = dbCollectionResult.Id;
            }

            return result;
        }

        public DbCollection GetCollection(string collectionId)
        {
            return _dataContext.GetQueryable<DbCollection>()
                .Include(c => c.Author)
                .Include(c => c.Tags)
                .Include(c => c.Images)
                .Where(c => c.Id == collectionId).FirstOrDefault();
        }

        public IQueryable<Collection> GetCollections()
        {
            return _dataContext.GetQueryable<DbCollection>()
                .Include(c => c.Author)
                .Include(c => c.Tags)
                .Include(c => c.Images)
                .MapToList<DbCollection, Collection>(_mapper).AsQueryable();
        }

        public bool Update(DbCollection collection)
        {
            if (collection == null)
            {
                return false;
            }

            DbCollection dbCollection = _dataContext.Update(collection);

            if (dbCollection == null)
            {
                return false;
            }

            return _dataContext.Save() != 0;
        }
    }
}
