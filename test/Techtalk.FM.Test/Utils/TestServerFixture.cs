using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using Techtalk.FM.API;
using Techtalk.FM.Domain.Contracts.Repositories;
using Techtalk.FM.Infra.Repositories.NHibernate;
using Techtalk.FM.Infra.Repositories.NHibernate.Migrations;

namespace Techtalk.FM.Test.Utils
{
    /// <summary>
    /// A Fixture class to resolve Server and Database to use on Tests
    /// </summary>
    public class TestServerFixture : IDisposable
    {
        /// <summary>
        /// TestServer created by Startup.cs
        /// </summary>
        public TestServer Server { get; private set; }

        /// <summary>
        /// HttpClient to test API on TestServer
        /// </summary>
        public HttpClient HttpClient { get; private set; }

        public TestServerFixture()
        {
            var configuration = ConfigurationHelper.GetIConfigurationRoot(Environment.CurrentDirectory);

            new MigrationHelper(configuration).RunMigrationDown();

            var builder = Program.CreateWebHostBuilder(new string[0])
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
