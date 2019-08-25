using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Techtalk.FM.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                   .ConfigureAppConfiguration((hostingContext, config) =>
                   {
                       var env = hostingContext.HostingEnvironment;

                       config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true);

                       config.AddEnvironmentVariables();
                   })
                   .UseStartup<Startup>();
        }           
    }
}
