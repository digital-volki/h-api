using System.Configuration;
using Leifez.DataAccess.Interfaces;
using Microsoft.Practices.Unity;
using DataContext = Leifez.DataAccess.PostgreSQL.DataContext;
namespace Leifez.Infrastructure.Unity
{
    public static class UnityConfig
    {
        public static void Initialize(IUnityContainer container)
        {
            RegisterDataContext(container);
        }
        private static void RegisterDataContext(IUnityContainer container)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            container.RegisterType<IDataContext, DataContext>(
                new InjectionConstructor(
                    connectionString)
                );
        }
    }
}
