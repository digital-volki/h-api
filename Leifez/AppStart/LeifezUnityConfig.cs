using Leifez.Infrastructure.Unity;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace Leifez.AppStart
{
    public static class LeifezUnityConfig
    {
        public static IUnityContainer RegisterComponents(HttpConfiguration httpConfiguration)
        {
            IUnityContainer container = new UnityContainer();

            httpConfiguration.DependencyResolver = new UnityResolver(container);

            container.RegisterType<IDependencyResolver, UnityResolver>(new PerResolveLifetimeManager());

            UnityConfig.Initialize(container);

            return container;
        }
    }
}
