using Leifez.Core.PostgreSQL.Configs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Leifez.Core.PostgreSQL.Models
{
    public class IdentityDbContext : IdentityDbContext<DbUser>
    {
        public new DbSet<DbUser> Users { get; set; }
        public new DbSet<DbRole> Roles { get; set; }
        public DbSet<DbCollection> Collections { get; set; }
        public DbSet<DbImage> Images { get; set; }
        public DbSet<DbTag> Tags { get; set; }
        public DbSet<DbLike> Likes { get; set; }

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
            builder.ApplyConfiguration(new LikeConfig());
        }
    }
}
