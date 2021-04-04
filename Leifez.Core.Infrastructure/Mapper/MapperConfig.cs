using AutoMapper;
using Leifez.Application.Domain.Mapping;
using System.Collections.Generic;

namespace Leifez.Core.Infrastructure.Mapper
{
    public static class MapperConfig
    {
        public static IMapper Initialize()
        {
            List<Profile> profiles = new List<Profile>();

            InitializeDomain(profiles);

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });

            return config.CreateMapper();
        }

        private static void InitializeDomain(ICollection<Profile> profiles)
        {
            profiles.Add(new TagMapping());
            profiles.Add(new CollectionMapping());
            profiles.Add(new ImageMapping());
            profiles.Add(new AccountMapping());
        }
    }
}
