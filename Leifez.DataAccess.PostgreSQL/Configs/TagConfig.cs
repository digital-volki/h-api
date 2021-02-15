using Leifez.DataAccess.PostgreSQL.Models;
using System.Data.Entity.ModelConfiguration;

namespace Leifez.DataAccess.PostgreSQL.Configs
{
    public class TagConfig : EntityTypeConfiguration<DbTag>
    {
        public TagConfig()
        {
            HasKey(x => x.TagId);
            Property(x => x.Title).IsOptional();
            Property(x => x.Type).IsOptional();
            Property(x => x.Quantity).IsOptional();
            Property(x => x.Danger).IsOptional();
        }
    }
}
