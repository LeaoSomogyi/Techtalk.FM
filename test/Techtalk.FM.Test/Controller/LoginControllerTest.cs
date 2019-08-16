using Newtonsoft.Json;
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
    public class LoginControllerTest : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;

        public LoginControllerTest(TestServerFixture fixture)
        {
            _fixture = fixture;
        }

        #region "  Ok  "

        [Fact]
        public async Task Login_Ok()
        {
            DTO.User user = new DTO.User() { Email = "felipe.somogyi@rakuten.com.br", Password = "12345678" };

            HttpResponseMessage response = await DoLogin(user);

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region "  NOk  "

        [Fact]
        public async Task Login_Email_Null_NOk()
        {
            var user = new DTO.User() { Password = "12345678" };

            var response = await DoLogin(user);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Login_Password_Null_NOk()
        {
            var user = new DTO.User() { Email = "felipe.somogyi@rakuten.com.br" };

            var response = await DoLogin(user);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Login_NOk()
        {
            DTO.User user = new DTO.User() { Email = "abobrinha@lala.com", Password = "123456" };

            HttpResponseMessage response = await DoLogin(user);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion

        #region "  Private Methods  "

        /// <summary>
        /// Method to do the Login request
        /// </summary>
        /// <param name="user">User DTO</param>
        /// <returns>HttpResponseMessage</returns>
        private async Task<HttpResponseMessage> DoLogin(DTO.User user)
        {
            StringContent payload = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            return await _fixture.HttpClient.PostAsync("api/login", payload);
        }

        #endregion
    }
}
