using Core.Models.Entities.Posts;
using DTO.Pagination;
using DTO.Posts;
using System.Threading.Tasks;

namespace Core.IRepositories.Posts
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<PagedListBase<DtoPostResponse>> GetAllPosts(PostParameters postParameters, int? userId);
    } 
}
