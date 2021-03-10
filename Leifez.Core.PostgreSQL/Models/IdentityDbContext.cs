using Leifez.Core.PostgreSQL.Configs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Leifez.Core.PostgreSQL.Models
{
    public class IdentityDbContext : IdentityDbContext<DbIdentityUser>
    {
        public new virtual DbSet<DbIdentityRole> Roles { get; set; }
        public virtual DbSet<DbCollection> Collections { get; set; }
        public virtual DbSet<DbTag> Tags { get; set; }
        public virtual DbSet<DbImage> Images { get; set; }

        public IdentityDbContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CollectionConfig());
            builder.ApplyConfiguration(new TagConfig());
            builder.ApplyConfiguration(new ImageConfig());
        }
    }
}
