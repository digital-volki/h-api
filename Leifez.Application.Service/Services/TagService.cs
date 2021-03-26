using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Leifez.Application.Domain.Interfaces;
using Leifez.Application.Domain.Models;
using Leifez.Application.Service.Interfaces;
using Leifez.Common.Mapping;
using Leifez.Core.PostgreSQL.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Leifez.Application.Service.Services
{
    public class TagService : ITagService
    {
        private readonly ITagDomain _tagDomain;
        private readonly IMapper _mapper;
        private readonly ILogger<TagService> _logger;

        public TagService(
            ITagDomain tagDomain,
            IMapper mapper,
            ILogger<TagService> logger)
        {
            _mapper = mapper;
            _tagDomain = tagDomain;
            _logger = logger;
        }

        public IEnumerable<Tag> GetAll()
        {
            return _tagDomain.GetAll();
        }

        public IEnumerable<Tag> Get(IEnumerable<int> ids)
        {
            return _tagDomain.Get(ids).MapToList<DbTag, Tag>(_mapper);
        }

        public Tag Get(int id)
        {
            return _tagDomain.Get(id).Map<DbTag, Tag>(_mapper);
        }
    }
}
