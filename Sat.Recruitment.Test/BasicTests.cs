using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Sat.Recruitment.Entities.Models;
using Sat.Recruitment.Exceptions.Common;
using Sat.Recruitment.Exceptions.Models;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class BasicTests : IClassFixture<WebApplicationFactory<Api.Startup>>
    {
        private readonly WebApplicationFactory<Api.Startup> _factory;

        public BasicTests(WebApplicationFactory<Api.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task UsersController_PostNotDuplicatedUser_CreateUser()
        {
            // Arrange
            var client = _factory.CreateClient();

            #region Request
            var newUser = new User()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = UserType.Normal,
                Money = 124
            };
            var requestContent = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");
            #endregion

            #region Expected
            var expectedUser = new User()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = UserType.Normal,
                Money = 223.2M
            };
            var expectedResponseContent = JsonConvert.SerializeObject(expectedUser);
            #endregion

            // Act
            var responseMessage = await client.PostAsync("/v1/users", requestContent);
            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            // Assert
            responseMessage.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, responseMessage.StatusCode);
            Assert.Equal(expectedResponseContent, responseContent);
        }

        [Fact]
        public async Task UsersController_PostDuplicatedUser_BadRequestUserDuplicated()
        {
            // Arrange
            var client = _factory.CreateClient();

            #region Request
            var newUser = new User()
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = UserType.Normal,
                Money = 124
            };
            var requestContent = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");
            #endregion

            // Act
            var responseMessage = await client.PostAsync("/v1/users", requestContent);
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            var responseError = JsonConvert.DeserializeObject<HttpResponseError>(responseContent);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, responseError.StatusCode);
            Assert.Equal("user_duplicated", responseError.Error);
            Assert.Equal("The user is duplicated.", responseError.Message);
        }
    }
}
