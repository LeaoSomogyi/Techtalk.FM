using Microsoft.AspNetCore.TestHost;
using System;

namespace Techtalk.FM.Test.Utils
{
    public static class TestExtensions
    {
        public static TService GetService<TService>(this TestServer server) where TService : class
        {
            return server?.Host?.Services?.GetService(typeof(TService)) as TService;
        }

        public static T Moq<T>(this T obj, Action<T> action)
        {
            action(obj);

            return obj;
        }
    }
}
