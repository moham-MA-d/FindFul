using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.Posts;
using Refit;

namespace SDK.Tester.Interfaces
{
    public interface IPostSdk
    {
        Task<ApiResponse<DtoPostResponse>> AddAsync();
        Task<ApiResponse<DtoPostResponse>> GetAsync(int postId);
        Task<ApiResponse<DtoPostResponse>> EditAsync(DtoPostRequest dtoPostRequest);
        Task<ApiResponse<IEnumerable<DtoPostResponse>>> GetAllAsync();
        Task<ApiResponse<string>> DeleteAsync(int postId);
        DtoPostRequest GetPostRequestFromPostResponse(DtoPostResponse response);
    }
}
