using Leifez.DataAccess.PostgreSQL.Models;
using System.Data.Entity.ModelConfiguration;

namespace Leifez.DataAccess.PostgreSQL.Configs
{
    public class UserRoleConfig : EntityTypeConfiguration<UserRole>
    {
        public UserRoleConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.UserId).IsUnicode().IsMaxLength().IsRequired();
            Property(x => x.RoleId).IsRequired();
        }
    }
}
