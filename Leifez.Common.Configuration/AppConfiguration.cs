using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Leifez.Common.Configuration
{
    public class AppConfiguration
    {
        private static AppConfiguration _singleton;
        private IConfiguration _configuration;
        private string _environmentName;

        public static IConfiguration Configuration { get => _singleton._configuration; }
        public static string EnvironmentName { get => _singleton._environmentName; }
        public static string DatabaseFactoryConnection
        {
            get
            {
                string envName = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                using (StreamReader reader = File.OpenText($"AppSettings.{envName}.json"))
                {
                    JObject obj = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                    JObject connectionStrings = obj.Value<JObject>("ConnectionStrings");
                    return connectionStrings.Value<string>("DefaultConnection");
                }
            }
        }
        public AppConfiguration(IConfiguration configuration, string environmentName)
        {
            _configuration = configuration;
            _environmentName = environmentName;

            _singleton = this;
        }
    }
}
