using AutoMapper;
using Leifez.Application.Domain.Interfaces;
using Leifez.Application.Domain.Models;
using Leifez.Common.Mapping;
using Leifez.Core.PostgreSQL;
using Leifez.Core.PostgreSQL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Leifez.Application.Domain
{
    public class TagDomain : ITagDomain
    {
        private readonly IDataContext _dataContext;
        private readonly IMapper _mapper;

        public TagDomain(
            IDataContext dataContext,
            IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public IEnumerable<Tag> GetAll()
        {
            var dbTags = _dataContext.GetQueryable<DbTag>();
            var imagesQuantities = _dataContext.GetQueryable<DbTag>().Select(t => new KeyValuePair<int, int>(t.Id, t.Images.Count)).ToDictionary(x => x.Key, x => x.Value);
            var tags = dbTags.MapToList<DbTag, Tag>(_mapper);
            tags.ForEach(t => t.Quantity = imagesQuantities[t.Id]);
            return tags;
        }

        public IEnumerable<DbTag> Get(IEnumerable<int> ids)
        {
            if (!ids.Any())
            {
                return null;
            }

            return _dataContext.GetQueryable<DbTag>()
                .Where(t => ids.Contains(t.Id));
        }

        public DbTag Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return _dataContext.GetQueryable<DbTag>()
                .Where(t => t.Id == id).FirstOrDefault();
        }

        public int GetQuantityImages(int tagId)
        {
            if (tagId <= 0)
            {
                return 0;
            }

            return _dataContext.GetQueryable<DbTag>().Where(t => t.Id == tagId).Select(t => t.Images.Count).FirstOrDefault();
        }
    }
}
