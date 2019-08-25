using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Techtalk.FM.Test.Utils;
using Xunit;
using DTO = Techtalk.FM.Domain.DTOs;

namespace Techtalk.FM.Test.Controller
{
    [ExcludeFromCodeCoverage]
    public class LoginControllerTest : BaseControllerTest
    {
        #region "  Constructors  "

        public LoginControllerTest(TestServerFixture fixture) : base(fixture) { }

        #endregion

        #region "  Ok  "

        [Fact]
        public async Task Login_Ok()
        {
            var user = new DTO.User() { Email = "felipe.somogyi@rakuten.com.br", Password = "12345678" };

            var response = await DoLogin(user);

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region "  NOk  "

        [Theory]
        [MemberData(nameof(Invalid_Users))]
        public async Task Login_Invalid_Data_NOk(DTO.User user)
        {
            var response = await DoLogin(user);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion

        #region "  Private Methods  "

        /// <summary>
        /// Method to do the Login Request POST api/login
        /// </summary>
        /// <param name="user">User DTO</param>
        /// <returns>HttpResponseMessage</returns>
        private async Task<HttpResponseMessage> DoLogin(DTO.User user)
        {
            var payload = new StringContent(JsonConvert.SerializeObject(user, Fixture.SerializerSettings), Encoding.UTF8, "application/json");

            return await Fixture.HttpClient.PostAsync("api/login", payload);
        }

        #endregion

        #region "  Theory  "

        public static IEnumerable<object[]> Invalid_Users()
        {
            return new List<object[]>()
            {
                new object[] { new DTO.User() { Password = "12345678" } },
                new object[] { new DTO.User() { Email = "felipe.somogyi@rakuten.com.br" } },
                new object[] { new DTO.User() { Email = "abobrinha@lala.com", Password = "123456" } },
                new object[] { new DTO.User() { Email = "lalala" } }
            };
        }

        #endregion
    }
}
