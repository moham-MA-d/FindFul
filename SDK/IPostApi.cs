using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.Pagination;
using DTO.Posts;
using Refit;

namespace SDK
{
    [Headers("Authorization: Bearer")]
    public interface IPostApi
    {
        [Post("/api/V1/Posts/Add")]
        Task<ApiResponse<DtoPostResponse>> AddAsync([Body] DtoPostRequest dtoPostRequest);

        [Get("/api/V1/Posts/{PostId}")]
        Task<ApiResponse<DtoPostResponse>> GetAsync(int postId);

        [Get("/api/V1/Posts")]
        Task<ApiResponse<IEnumerable<DtoPostResponse>>> GetAllAsync([Query] PostParameters postParameters);

        [Put("/api/V1/Posts/Edit")]
        Task<ApiResponse<DtoPostResponse>> EditAsync([Body] DtoPostRequest dtoPostRequest);

        [Delete("/api/V1/Posts/{PostId}")]
        Task<ApiResponse<string>> DeleteAsync(int postId);
    }
}
