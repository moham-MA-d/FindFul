using System;
using DTO.Posts;
using Swashbuckle.AspNetCore.Filters;

namespace API.SwaggerExamples.Requests
{
    public class CreatePostRequestExample : IExamplesProvider<DtoPostRequest>
    {
        public DtoPostRequest GetExamples()
        {
            var postRequest =  new DtoPostRequest
            {
                Body = "This is a new post request example.",
                CreateDateTime = DateTime.Now
            };
            return postRequest;
        }
    }
}