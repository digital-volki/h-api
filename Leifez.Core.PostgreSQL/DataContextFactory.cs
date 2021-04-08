using Leifez.Common.Configuration;
using Leifez.Core.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Leifez.DataAccess.PostgreSQL
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseNpgsql(AppConfiguration.DatabaseFactoryConnection);

            return new DataContext(optionsBuilder.Options);
        }
    }
}
