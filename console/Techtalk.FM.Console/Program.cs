using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Techtalk.FM.IoC;

namespace Techtalk.FM.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    InjectorBootStrapper.RegisterServices(services);

                    services.AddSingleton<IHostedService, MigrationRunner>();
                })
                .RunConsoleAsync();
        }
    }
}
