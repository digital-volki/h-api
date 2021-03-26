using AutoMapper;
using Leifez.Application.Domain.Models;
using Leifez.Core.PostgreSQL.Models;
using System.Linq;

namespace Leifez.Application.Domain.Mapping
{
    public class CollectionMapping : Profile
    {
        public CollectionMapping()
        {
            CreateMap<DbCollection, Collection>()
                .ForMember(c => c.AuthorId, opt => opt.MapFrom(dc => dc.Author.Id))
                .ForMember(c => c.Images, opt => opt.MapFrom(dc => dc.Images.Select(i => i.Guid)))
                .ForMember(c => c.Tags, opt => opt.MapFrom(dc => dc.Tags.Select(i => i.Id)));

            CreateMap<Collection, DbCollection>()
                .ForMember(c => c.Images, opt => opt.Ignore())
                .ForMember(c => c.Tags, opt => opt.Ignore());
        }
    }
}
