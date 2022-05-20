using Core;
using Core.IRepositories.Posts;
using Core.IServices.Posts;
using Core.Models.Entities.Posts;
using DTO.Pagination;
using DTO.Posts;
using System.Threading.Tasks;
using Core.IServices;

namespace Service.Classes.Posts
{
    public class PostService : EntityService<Post>, IPostService, IEntityService<Post>
    {
        readonly IPostRepository _postRepository;
        public PostService(IUnitOfWork unitOfWork, IPostRepository postRepository) : base(unitOfWork, postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<PagedListBase<DtoPostResponse>> GetAllPosts(PostParameters postParameters, int? userId)
        {
            return await _postRepository.GetAllPosts(postParameters, userId);
        }
    }
}
