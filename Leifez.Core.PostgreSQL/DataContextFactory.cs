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
            optionsBuilder.UseNpgsql(AppConfiguration.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
            //optionsBuilder.UseNpgsql("Server=192.168.31.235;User Id=site_db;Password=JGJS89ydhnflsar312h89HLFDF2;Port=5432;Database=site_db;");

            return new DataContext(optionsBuilder.Options);
        }
    }
}
