using Leifez.DataAccess.PostgreSQL.Models;
using System.Data.Entity.ModelConfiguration;

namespace Leifez.DataAccess.PostgreSQL.Configs
{
    public class CollectionConfig : EntityTypeConfiguration<DbCollection>
    {
        public CollectionConfig()
        {
            HasKey(x => x.CollectionId);
            Property(x => x.Title).IsOptional();
            Property(x => x.Description).IsOptional();
            Property(x => x.Author).IsOptional();
        }
    }
}
