using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using Techtalk.FM.API;
using Techtalk.FM.Domain.Contracts.Repositories;
using Techtalk.FM.Infra.Repositories.NHibernate;

namespace Techtalk.FM.Test.Utils
{
    /// <summary>
    /// A Fixture class to resolve Server and Database to use on Tests
    /// </summary>
    public class TestServerFixture : IDisposable
    {
        #region "  Properties  "

        /// <summary>
        /// TestServer created by Startup.cs
        /// </summary>
        public TestServer Server { get; private set; }

        /// <summary>
        /// HttpClient to test API on TestServer
        /// </summary>
        public HttpClient HttpClient { get; private set; }

        /// <summary>
        /// Json Serializer options adding SnakeCaseNamingStrategy
        /// </summary>
        public JsonSerializerSettings SerializerSettings
        {
            get
            {
                return new JsonSerializerSettings()
                {
                    ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() },
                    DateFormatString = "yyyy-MM-ddTHH:mm:ss",
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
            }
        }

        #endregion

        #region "  Constructors  "

        public TestServerFixture()
        {
            var configuration = ConfigurationHelper.GetIConfigurationRoot(Environment.CurrentDirectory);

            var builder = Program.CreateWebHostBuilder(new string[0])
                .ConfigureTestServices((IServiceCollection services) =>
                {
                    services.AddSingleton<IUnitOfWork, UnitOfWork>();
                })
                .UseStartup<Startup>();

            Server = new TestServer(builder);

            HttpClient = Server.CreateClient();
        }

        #endregion

        #region "  Public Methods  "

        public HttpClient SetRequestAuthorization(string token)
        {
            if (!HttpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }            

            return HttpClient;
        }

        #endregion

        #region "  IDisposable  "

        public void Dispose()
        {
            Server.Dispose();
            HttpClient.Dispose();
        }

        #endregion
    }
}
