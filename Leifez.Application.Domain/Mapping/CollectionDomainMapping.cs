using AutoMapper;
using Leifez.Application.Domain.Models;
using Leifez.DataAccess.PostgreSQL.Models;

namespace Leifez.Application.Domain.Mapping
{
    public class CollectionDomainMapping : Profile
    {
        public CollectionDomainMapping()
        {
            CreateMap<Collection, DbCollection>()
                .ForMember(a => a.CollectionId, opt => opt.MapFrom(b => b.Id))
                .ForMember(a => a.Title, opt => opt.MapFrom(b => b.Title))
                .ForMember(a => a.Description, opt => opt.MapFrom(b => b.Description))
                .ForMember(a => a.Author, opt => opt.MapFrom(b => b.Author))
                .ForMember(a => a.Image, opt => opt.MapFrom(b => b.Image))
                .ForMember(a => a.Tags, opt => opt.MapFrom(b => b.Tags));
        }
    }
}
