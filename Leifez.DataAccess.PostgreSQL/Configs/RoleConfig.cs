using Leifez.DataAccess.PostgreSQL.Models;
using System.Data.Entity.ModelConfiguration;

namespace Leifez.DataAccess.PostgreSQL.Configs
{
    public class RoleConfig : EntityTypeConfiguration<Role>
    {
        public RoleConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Name).IsUnicode().IsMaxLength().IsRequired();
        }
    }
}
