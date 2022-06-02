using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using API.IntegrationTests.Helper;
using Core.Models.Entities.Posts;
using DTO.Posts;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Xunit;

namespace API.IntegrationTests.Controllers.Version1
{
    public class PostsControllerTest : IntegrationTestBase/*<Startup>*/
    {
      
        [Fact]
        public async Task Add_CorrectScenario_ReturnPost()
        {
            //Arrange
            PostHelper _postHelper = new PostHelper();
            await AuthenticateAsync();
            var createdPost = await _postHelper.AddPost(new DtoPostRequest
            {
                Id = 1,
                Body = "Body",
                CreateDateTime = DateTime.Now
            });

            //Act   
            var response = await _httpClient.GetAsync($"api/v1/Post/Get/{createdPost.Id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedPost = await response.Content.ReadFromJsonAsync<Post>();
            if (returnedPost != null)
            {
                returnedPost.Id.Should().Be(createdPost.Id);
                returnedPost.Body.Should().Be("Body");
            }
        }
    }
}
