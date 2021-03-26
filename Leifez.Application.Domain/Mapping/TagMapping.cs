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
                .ForMember(t => t.Quantity, opt => opt.MapFrom(dt => dt.Images.Count));
            CreateMap<Tag, DbTag>();
        }
    }
}
