using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
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
    public class BookControllerTest : BaseControllerTest
    {
        #region "  Constructors  "

        public BookControllerTest(TestServerFixture fixture) : base(fixture) { }

        #endregion

        #region "  Ok  "

        [Fact]
        public async Task Save_Book_Ok()
        {
            var response = await SaveBook();

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_Book_Ok()
        {
            var task = Record.ExceptionAsync(async () =>
            {
                var book = await SaveAndReturnBook();

                //Call GET - api/book/{id}
                var response = await Fixture.HttpClient.GetAsync($"api/book/{book.Id}");

                //Convert HttpContent to string
                var content = await response.Content.ReadAsStringAsync();

                var json = JObject.Parse(content);

                //After parse to json, convert do DTO Book
                var _book = JsonConvert.DeserializeObject<DTO.Book>(json["value"].ToString(), Fixture.SerializerSettings);

                Assert.Equal(book.Id, _book.Id);
            });

            var ex = await task;

            Assert.True(ex == null);
        }

        [Fact]
        public async Task Delete_Book_Ok()
        {
            var book = await SaveAndReturnBook();

            //Call DELETE - api/book/{id}
            var response = await Fixture.HttpClient.DeleteAsync($"api/book/{book.Id}");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region "  NOk  "

        [Theory]
        [MemberData(nameof(Invalid_Books))]
        public async Task Book_Invalid_Data_NOk(DTO.Book book)
        {
            var token = await GetToken();

            var client = Fixture.SetRequestAuthorization(token.AccessToken);

            var payload = new StringContent(JsonConvert.SerializeObject(book, Fixture.SerializerSettings), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/book", payload);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion

        #region "  Private Methods  "

        /// <summary>
        /// Get Book DTO
        /// </summary>
        /// <returns>Book DTO</returns>
        private static DTO.Book GetBook()
        {
            return new DTO.Book()
            {
                Title = "The Witcher - A Torre da Andorinha",
                Subtitle = "A Saga do bruxo Geralt de Rívia",
                Author = "Andrzej Sapkowski",
                PublishDate = DateTime.Now,
                ISBN = "9788546900978",
                PageNumber = 456,
                PublishingHouse = "WMF Martins Fontes"
            };
        }

        /// <summary>
        /// Method to do the Save Request POST - api/book
        /// </summary>
        /// <returns>HttpResponseMessage</returns>
        private async Task<HttpResponseMessage> SaveBook()
        {
            var token = await GetToken();

            var client = Fixture.SetRequestAuthorization(token.AccessToken);

            var payload = new StringContent(JsonConvert.SerializeObject(GetBook(), Fixture.SerializerSettings), Encoding.UTF8, "application/json");

            return await client.PostAsync("api/book", payload);
        }

        /// <summary>
        /// Save Book and return it
        /// </summary>
        /// <returns>DTO Book</returns>
        private async Task<DTO.Book> SaveAndReturnBook()
        {
            var response = await SaveBook();

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            //Convert HttpContent to string
            var content = await response.Content.ReadAsStringAsync();

            var json = JObject.Parse(content);

            return JsonConvert.DeserializeObject<DTO.Book>(json["value"].ToString(), Fixture.SerializerSettings);
        }

        /// <summary>
        /// Get Access Token from POST - api/login
        /// </summary>
        /// <returns>DTO Token</returns>
        private async Task<DTO.Token> GetToken()
        {
            var _user = new DTO.User() { Email = "felipe.somogyi@rakuten.com.br", Password = "12345678" };

            var user = new StringContent(JsonConvert.SerializeObject(_user, Fixture.SerializerSettings), Encoding.UTF8, "application/json");

            var response = await Fixture.HttpClient.PostAsync("api/login", user);

            var content = await response.Content.ReadAsStringAsync();

            var json = JObject.Parse(content);

            return JsonConvert.DeserializeObject<DTO.Token>(json["value"].ToString(), Fixture.SerializerSettings);
        }

        #endregion

        #region "  Theory  "

        public static IEnumerable<object[]> Invalid_Books()
        {
            return new List<object[]>()
            {
                new object[] { GetBook().Moq((b) => b.Title = null) },
                new object[] { GetBook().Moq((b) => b.Subtitle = null) },
                new object[] { GetBook().Moq((b) => b.Author = null) },
                new object[] { GetBook().Moq((b) => b.PublishDate = DateTime.MinValue) },
                new object[] { GetBook().Moq((b) => b.PageNumber = 0) },
                new object[] { GetBook().Moq((b) => b.PublishingHouse = null) },
                new object[] { GetBook().Moq((b) => b.ISBN = null) },
                new object[] { GetBook().Moq((b) => b.Title = string.Empty.GenerateRandomCharacters(251)) },
                new object[] { GetBook().Moq((b) => b.Subtitle = string.Empty.GenerateRandomCharacters(251)) },
                new object[] { GetBook().Moq((b) => b.Author = string.Empty.GenerateRandomCharacters(151)) },
                new object[] { GetBook().Moq((b) => b.ISBN = string.Empty.GenerateRandomCharacters(14)) }
            };
        }

        #endregion
    }
}
