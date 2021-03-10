using AutoMapper;
using Leifez.Application.Domain.Interfaces;
using Leifez.Application.Domain.Models;
using Leifez.Common.Mapping;
using Leifez.Core.PostgreSQL;
using Leifez.Core.PostgreSQL.Models;
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

        public IEnumerable<Tag> GetTags()
        {
            return _dataContext.GetQueryable<DbTag>().MapToList<DbTag, Tag>(_mapper);
        }
    }
}
