using System;
using DTO.Posts;
using Swashbuckle.AspNetCore.Filters;

namespace API.SwaggerExamples.Requests;

public class PostRequestExamples
{
    public class CreatePostRequestExample : IExamplesProvider<DtoPostRequest>
    {
        public DtoPostRequest GetExamples()
        {
            return new DtoPostRequest
            {
                Body = "This is a new post request example.",
                CreateDateTime = DateTime.Now
            };
        }
    }
}