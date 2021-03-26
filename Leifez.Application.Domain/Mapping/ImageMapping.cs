using AutoMapper;
using Leifez.Application.Domain.Models;
using Leifez.Core.PostgreSQL.Models;

namespace Leifez.Application.Domain.Mapping
{
    public class ImageMapping : Profile
    {
        public ImageMapping()
        {
            CreateMap<DbImage, Image>();
            CreateMap<Image, DbImage>();
        }
    }
}
