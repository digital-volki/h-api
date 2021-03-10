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
                .ForMember(a => a.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(a => a.Title, opt => opt.MapFrom(b => b.Title))
                .ForMember(a => a.Description, opt => opt.MapFrom(b => b.Description))
                .ForMember(a => a.Author, opt => opt.MapFrom(b => b.Author))
                .ForMember(a => a.Image, opt => opt.MapFrom(b => b.Image));
                //.ForMember(a => a.Tags, opt => opt.MapFrom(b => b.Tags));
        }
    }
}
