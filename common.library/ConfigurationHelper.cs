using Microsoft.Extensions.Configuration;

namespace common.library
{
    public static class ConfigurationHelper
    {
        public static IConfiguration _configuration;
        public static void Initialize(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }
    }
}
