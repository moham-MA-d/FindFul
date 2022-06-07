using DTO.Account;
using DTO.Posts;
using Swashbuckle.AspNetCore.Filters;

namespace API.SwaggerExamples.Responses
{
    public class PostResponseExamples : IExamplesProvider<DtoPostResponse>
    {
        public DtoPostResponse GetExamples()
        {
            var postResponse = new DtoPostResponse
            {
                Body = "This is a post response example.",
                TheUser = new DtoMember()
            };
            return postResponse;
        }
    }
}
