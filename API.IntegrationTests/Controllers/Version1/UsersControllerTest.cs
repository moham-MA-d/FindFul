using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using DTO.Account;
using FluentAssertions;
using Xunit;

namespace API.IntegrationTests.Controllers.Version1
{
    public class UsersControllerTest : IntegrationTestBase
    {
        public async Task GetAll_X_Y()
        {
            //Arrange

            //Act

            //Assert
        }

        [Fact]
        public async Task GetAll_WithoutAnyUsers_ReturnEmptyResponse()
        {
            //Arrange
            await AuthenticateAsync();

            //Act
            var response = await _httpClient.GetAsync("api/v1/Users/GetAll");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadFromJsonAsync<List<DtoMember>>()).Should().NotBeNull();
        }
    }
}
