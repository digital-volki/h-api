using Microsoft.Extensions.Configuration;

namespace Leifez.Common.Configuration
{
    public class AppConfiguration
    {
        private static AppConfiguration _singleton;
        private IConfiguration _configuration;
        private string _environmentName;

        public static IConfiguration Configuration { get => _singleton._configuration; }
        public static string EnvironmentName { get => _singleton._environmentName; }

        public AppConfiguration(IConfiguration configuration, string environmentName)
        {
            _configuration = configuration;
            _environmentName = environmentName;

            _singleton = this;
        }
    }
}
