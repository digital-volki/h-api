using Leifez.Common.Web.BearerAuth.Filters;
using System.Web.Http;
using Unity.AspNet.WebApi;

namespace Leifez.AppStart
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new LeifezAuthenticateAttribute());
            config.DependencyResolver = new UnityDependencyResolver(LeifezUnityConfig.GetConfiguredContainer());
        }
    }
}
