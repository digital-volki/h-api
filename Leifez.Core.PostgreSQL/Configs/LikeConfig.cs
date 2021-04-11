using Leifez.Core.PostgreSQL.Models;
using Leifez.Core.PostgreSQL.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Leifez.Core.PostgreSQL.Configs
{
    public class LikeConfig : IEntityTypeConfiguration<DbLike>
    {
        public void Configure(EntityTypeBuilder<DbLike> builder)
        {
            builder.HasKey(l => l.HashId);
            builder.HasIndex(l => l.UserId);
            builder.HasIndex(l => new { l.EntityId, l.ContentType});
            builder.HasIndex(l => new { l.UserId, l.ContentType});
            builder.Property(l => l.HashId).HasMaxLength(32);
            builder.Property(x => x.EntityId).HasMaxLength(36).IsRequired();
            builder.Property(x => x.UserId).HasMaxLength(36).IsRequired();
            builder.Property(x => x.ContentType).IsRequired().HasConversion(
                v => v.ToString(),
                v => (ContentType)Enum.Parse(typeof(ContentType), v));
        }
    }
}
