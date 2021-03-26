using AutoMapper;
using Leifez.Application.Domain.Models;
using Leifez.Core.PostgreSQL.Models;

namespace Leifez.Application.Domain.Mapping
{
    public class CollectionMapping : Profile
    {
        public CollectionMapping()
        {
            CreateMap<DbCollection, Collection>()
                .ForMember(c => c.AuthorId, opt => opt.MapFrom(dc => dc.Author.UserName));

            CreateMap<Collection, DbCollection>()
                .ForMember(c => c.Images, opt => opt.Ignore())
                .ForMember(c => c.Tags, opt => opt.Ignore());
        }
    }
}
