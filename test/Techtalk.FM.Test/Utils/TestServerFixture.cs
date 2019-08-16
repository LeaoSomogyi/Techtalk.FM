using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using Techtalk.FM.API;
using Techtalk.FM.Domain.Contracts.Migrations;
using Techtalk.FM.Domain.Contracts.Repositories;
using Techtalk.FM.Infra.Repositories.NHibernate;
using Techtalk.FM.Infra.Repositories.NHibernate.Migrations;

namespace Techtalk.FM.Test.Utils
{
    public class TestServerFixture : IDisposable
    {
        public TestServer Server { get; private set; }

        public HttpClient HttpClient { get; private set; }

        public TestServerFixture()
        {
            IConfiguration configuration = ConfigurationHelper.GetIConfigurationRoot(Environment.CurrentDirectory);

            new MigrationHelper(configuration).RunMigrationDown();

            IWebHostBuilder builder = Program.CreateWebHostBuilder(new string[0])
                .ConfigureTestServices((IServiceCollection services) => 
                {
                    services.AddSingleton<IUnitOfWork, UnitOfWork>();
                })
                .UseStartup<Startup>();

            Server = new TestServer(builder);            

            HttpClient = Server.CreateClient();
        }

        public void Dispose()
        {
            Server.Dispose();
            HttpClient.Dispose();
        }
    }
}
