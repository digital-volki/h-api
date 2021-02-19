using Leifez.DataAccess.PostgreSQL.Models;
using Leifez.DataAccess.PostgreSQL.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Leifez.DataAccess.PostgreSQL.Configs
{
    public class TagConfig : IEntityTypeConfiguration<DbTag>
    {
        public void Configure(EntityTypeBuilder<DbTag> builder)
        {
            builder.HasKey(x => x.TagId);
            builder.Property(x => x.Title);
            builder.Property(x => x.Type).HasConversion(
                v => v.ToString(),
                v => (TagsType)Enum.Parse(typeof(TagsType), v));
            builder.Property(x => x.Quantity);
            builder.Property(x => x.Danger);
        }
    }
}
