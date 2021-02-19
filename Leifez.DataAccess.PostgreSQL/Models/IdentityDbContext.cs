using Leifez.DataAccess.PostgreSQL.Configs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Leifez.DataAccess.PostgreSQL.Models
{
    public class IdentityDbContext : IdentityDbContext<DbIdentityUser>
    {
        public new virtual DbSet<DbIdentityRole> Roles { get; set; }
        public virtual DbSet<DbCollection> Collections { get; set; }
        public virtual DbSet<DbTag> Tags { get; set; }

        public IdentityDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public IdentityDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfig());
            builder.ApplyConfiguration(new UserRoleConfig());
            builder.ApplyConfiguration(new CollectionConfig());
            builder.ApplyConfiguration(new TagConfig());
        }
    }
}
