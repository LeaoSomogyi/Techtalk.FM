using Microsoft.AspNetCore.TestHost;

namespace Techtalk.FM.Test.Utils
{
    /// <summary>
    /// Utils extensions to use on Test scenarios
    /// </summary>
    public static class TestExtensions
    {
        /// <summary>
        /// Get Service from TestServer
        /// </summary>
        /// <typeparam name="TService">Service to be retrieved</typeparam>
        /// <param name="server">Current TestServer</param>
        /// <returns>The Service</returns>
        public static TService GetService<TService>(this TestServer server) where TService : class
        {
            return server?.Host?.Services?.GetService(typeof(TService)) as TService;
        }
    }
}
