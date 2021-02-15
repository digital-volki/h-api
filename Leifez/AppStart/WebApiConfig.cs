using Leifez.Common.Web.BearerAuth.Filters;
using System.Web.Http;

namespace Leifez.AppStart
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new LeifezAuthenticateAttribute());
        }
    }
}
