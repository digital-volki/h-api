using Leifez.DataAccess.PostgreSQL.Configs;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Leifez.DataAccess.PostgreSQL.Models
{
    public class IdentityDbContext : IdentityDbContext<DbIdentityUser>
    {
        public new virtual IDbSet<DbIdentityRole> Roles { get; set; }
        public virtual IDbSet<DbCollection> Collections { get; set; }
        public virtual IDbSet<DbTag> Tags { get; set; }

        public IdentityDbContext()
            : base("site_db", false)
        {
        }

        public IdentityDbContext(string connectionString)
            : base(connectionString, false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new RoleConfig());
            modelBuilder.Configurations.Add(new UserRoleConfig());
            modelBuilder.Configurations.Add(new CollectionConfig());
            modelBuilder.Configurations.Add(new TagConfig());
        }
    }
}
