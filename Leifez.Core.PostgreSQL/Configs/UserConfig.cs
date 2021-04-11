using Leifez.Core.PostgreSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Leifez.Core.PostgreSQL.Configs
{
    public class UserConfig : IEntityTypeConfiguration<DbUser>
    {
        public void Configure(EntityTypeBuilder<DbUser> builder)
        {
            builder
                .HasMany(u => u.Collections)
                .WithMany(c => c.Users);
        }
    }
}
