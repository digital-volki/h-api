using System.Configuration;
using System.Web.Mvc;
using Leifez.Application.Service.Interfaces;
using Leifez.Application.Service.Services;
using Leifez.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity.Lifetime;
using DataContext = Leifez.DataAccess.PostgreSQL.DataContext;

namespace Leifez.Infrastructure.Unity
{
    public static class UnityConfig
    {
        public static void Initialize(IUnityContainer container)
        {
            RegisterDataContext(container);
            RegisterQueries(container);
        }

        private static void RegisterQueries(IUnityContainer container)
        {
            container.RegisterType<ICollectionService, CollectionService>();
        }

        private static void RegisterDataContext(IUnityContainer container)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

            var options = optionsBuilder
                .UseNpgsql(connectionString)
                .Options;

            container.RegisterType<IDataContext, DataContext>(new TransientLifetimeManager(),
                new InjectionConstructor(
                    options)
                ,
                new InjectionConstructor(
                    connectionString));
        }
    }
}
