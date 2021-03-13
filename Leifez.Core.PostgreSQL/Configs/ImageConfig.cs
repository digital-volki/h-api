using Leifez.Core.PostgreSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Leifez.Core.PostgreSQL.Configs
{
    public class ImageConfig : IEntityTypeConfiguration<DbImage>
    {
        public void Configure(EntityTypeBuilder<DbImage> builder)
        {
            builder.ToTable("Images");
            builder.HasKey(x => x.Guid);
            builder.Property(x => x.Guid).HasMaxLength(36);
            builder.Property(x => x.Hash).HasMaxLength(32).IsRequired();
        }
    }
}
