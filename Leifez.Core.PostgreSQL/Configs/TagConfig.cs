using Leifez.Core.PostgreSQL.Models;
using Leifez.Core.PostgreSQL.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Leifez.Core.PostgreSQL.Configs
{
    public class TagConfig : IEntityTypeConfiguration<DbTag>
    {
        public void Configure(EntityTypeBuilder<DbTag> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
            builder.Property(x => x.Type).HasConversion(
                v => v.ToString(),
                v => (TagsType)Enum.Parse(typeof(TagsType), v));
            builder.Property(x => x.Quantity);
            builder.Property(x => x.Danger);
        }
    }
}
