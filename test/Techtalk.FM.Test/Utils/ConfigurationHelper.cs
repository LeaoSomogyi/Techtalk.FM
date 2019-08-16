using Microsoft.Extensions.Configuration;

namespace Techtalk.FM.Test.Utils
{
    public class ConfigurationHelper
    {
        public static IConfiguration GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .SetBasePath(outputPath)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
