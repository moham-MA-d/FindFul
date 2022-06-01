using System.Net.Http.Json;
using System.Threading.Tasks;
using DTO.Posts;

namespace API.IntegrationTests.Helper
{
    public class PostHelper : IntegrationTestBase/*<Startup>*/
    {
        public async Task<DtoPostResponse> AddPost(DtoPostRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/v1/Post/Add", request);
            return await response.Content.ReadFromJsonAsync<DtoPostResponse>();
        }
    }
}
