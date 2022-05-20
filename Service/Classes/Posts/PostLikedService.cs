using Core;
using Core.IRepositories.Posts;
using Core.IServices.Posts;
using Core.Models.Entities.Posts;
using DTO.Account.Base;
using DTO.Pagination;
using DTO.Posts;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.IServices;

namespace Service.Classes.Posts
{
	public class PostLikedService : EntityService<PostLiked>, IPostedLikedService, IEntityService<PostLiked>
	{
		private readonly IPostLikedRepository _postLikedRepository;

		public PostLikedService(IUnitOfWork unitOfWork, IPostLikedRepository postLikedRepository) : base(unitOfWork, postLikedRepository)
		{
			_postLikedRepository = postLikedRepository;
		}

        public async Task<PostLiked> GetPostLike(int postId, int userId)
        {
           return await _postLikedRepository.GetPostLike(postId, userId);
        }

        public async Task<IEnumerable<DtoPostRequest>> GetPostsLikedByUser(int userId, PostParameters postParameters)
        {
            return await _postLikedRepository.GetPostsLikedByUser(userId, postParameters);
        }

        public async Task<IEnumerable<BaseMemberDTO>> GetUsersLikedPost(int postId)
        {
            return await _postLikedRepository.GetUsersLikedPost(postId);
        }
    }
}
