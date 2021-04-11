using Leifez.Core.PostgreSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Leifez.Core.PostgreSQL.Configs
{
    public class CollectionConfig : IEntityTypeConfiguration<DbCollection>
    {
        public void Configure(EntityTypeBuilder<DbCollection> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.Title).HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(400);

            builder
                .HasOne(c => c.Author)
                .WithMany(u => u.CreatedCollections);
        }
    }
}
