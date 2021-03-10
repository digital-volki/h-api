using AutoMapper;
using Leifez.Application.Domain.Models;
using Leifez.Core.PostgreSQL.Models;

namespace Leifez.Application.Domain.Mapping
{
    public class TagMapping : Profile
    {
        public TagMapping()
        {
            CreateMap<DbTag, Tag>()
                .ForMember(a => a.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(a => a.Name, opt => opt.MapFrom(b => b.Name))
                .ForMember(a => a.Type, opt => opt.MapFrom(b => b.Type))
                .ForMember(a => a.Danger, opt => opt.MapFrom(b => b.Danger));
        }
    }
}
