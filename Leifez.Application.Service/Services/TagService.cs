using AutoMapper;
using Leifez.Application.Domain.Interfaces;
using Leifez.Application.Domain.Models;
using Leifez.Application.Service.Interfaces;
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

        public IEnumerable<Tag> GetTags()
        {
            return _tagDomain.GetTags();
        }
    }
}
