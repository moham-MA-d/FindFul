using Core.IService;
using Core.Models.Entities.Posts;
using DTO.Pagination;
using DTO.Posts;
using System.Threading.Tasks;

namespace Core.IServices.Posts
{
    public interface IPostService : IEntityService<Post>
    {
        Task<PagedListBase<PostsDTO>> GetAllPosts(PostParameters postParameters);

    }
}
