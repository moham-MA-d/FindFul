using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.Pagination;
using DTO.Posts;
using Refit;
using SDK.Tester.Interfaces;

namespace SDK.Tester.Classes
{


    public class PostSdk : IPostSdk
    {
        private readonly IPostApi _postApi;
       
        public PostSdk(string cachedToken)
        {
            _postApi = RestService.For<IPostApi>("https://localhost:44341", new RefitSettings
            {
                //it release a delegate that return a string that put in authorization header.
                AuthorizationHeaderValueGetter = () => Task.FromResult(cachedToken)
            });
        }


        public async Task<ApiResponse<DtoPostResponse>> AddAsync()
        {
            var post = await _postApi.AddAsync(new DtoPostRequest
            {
                Body = "Post body from sdk",
            });
            return post;
        }
        public async Task<ApiResponse<DtoPostResponse>> GetAsync(int postId)
        {
            var post = await _postApi.GetAsync(postId);

            return post;
        }
        public async Task<ApiResponse<DtoPostResponse>> EditAsync(DtoPostRequest dtoPostRequest)
        {
            dtoPostRequest.Body = "Post body from sdk is edited";
            var post = await _postApi.EditAsync(dtoPostRequest);

            return post;
        }
        public async Task<ApiResponse<IEnumerable<DtoPostResponse>>> GetAllAsync()
        {
            var postParameters = new PostParameters();
            var posts = await _postApi.GetAllAsync(postParameters);

            return posts;
        }
        public async Task<ApiResponse<string>> DeleteAsync(int postId)
        {
            var r = await _postApi.DeleteAsync(postId);
            return r;
        }

        public DtoPostRequest GetPostRequestFromPostResponse(DtoPostResponse response)
        {
            return null;
            //return _mapperService.DtoPostResponseToDtoPostRequest(response);
        }
    }
}
