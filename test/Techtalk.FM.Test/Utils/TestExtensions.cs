using Microsoft.AspNetCore.TestHost;
using System;
using System.Linq;

namespace Techtalk.FM.Test.Utils
{
    /// <summary>
    /// Utils extensions to use on Test scenarios
    /// </summary>
    public static class TestExtensions
    {
        /// <summary>
        /// Get service from Host ServiceCollection
        /// </summary>
        /// <typeparam name="TService">Service Required</typeparam>
        /// <param name="server">Current host server</param>
        /// <returns>The Service required</returns>
        public static TService GetService<TService>(this TestServer server) where TService : class
        {
            return server?.Host?.Services?.GetService(typeof(TService)) as TService;
        }

        /// <summary>
        /// Moq some action in a object
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="obj">Object</param>
        /// <param name="action">Action to be applied</param>
        /// <returns>Object with action applied</returns>
        public static T Moq<T>(this T obj, Action<T> action)
        {
            action(obj);

            return obj;
        }

        /// <summary>
        /// Generated random characters based on length informed
        /// </summary>
        /// <param name="value">Current string instance</param>
        /// <param name="length">Length of the string</param>
        /// <returns>Random string generated</returns>
        public static string GenerateRandomCharacters(this string value, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            value = new string(Enumerable.Repeat(chars, length).Select(s => s[new Random().Next(s.Length)]).ToArray());

            return value;
        }
    }
}
