using Leifez.Infrastructure.Unity;
using System;
using Unity;

namespace Leifez.AppStart
{
    public static class LeifezUnityConfig
    {
        private static readonly IUnityContainer _container = new UnityContainer();
        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        public static IUnityContainer RegisterComponents()
        {
            UnityConfig.Initialize(_container);

            return _container;
        }

        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            if (_container != null)
            {
                return _container;
            }

            return RegisterComponents();
        });
    }
}
