using Leifez.DataAccess.PostgreSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Leifez.DataAccess.PostgreSQL.Configs
{
    public class CollectionConfig : IEntityTypeConfiguration<DbCollection>
    {
        public void Configure(EntityTypeBuilder<DbCollection> builder)
        {
            builder.HasKey(x => x.CollectionId);
            builder.Property(x => x.Title);
            builder.Property(x => x.Description);
            builder.Property(x => x.Author);
        }
    }
}
