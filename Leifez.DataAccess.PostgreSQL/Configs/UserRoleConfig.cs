using Leifez.DataAccess.PostgreSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Leifez.DataAccess.PostgreSQL.Configs
{
    public class UserRoleConfig : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId).IsUnicode().HasMaxLength(36).IsRequired();
            builder.Property(x => x.RoleId).IsRequired();
        }
    }
}
