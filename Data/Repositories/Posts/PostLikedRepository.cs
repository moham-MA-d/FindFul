using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.IRepositories.Posts;
using Core.Models.Entities.Posts;
using Data.Helpers;
using DTO.Account.Base;
using DTO.Pagination;
using DTO.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories.Posts
{
	public class PostLikedRepository : GenericRepository<PostLiked>, IPostLikedRepository
	{
        private readonly IMapper _mapper;

        public PostLikedRepository(DataContext context, ILogger logger, IMapper mapper) : base(context, logger)
		{
            _mapper = mapper;
        }

        public async Task<PostLiked> GetPostLike(int postId, int userId)
        {
           return await _context.PostsLiked.FindAsync(postId, userId);
        }

        public async Task<IEnumerable<PostsDTO>> GetPostsLikedByUser(int userId, PostParameters postParameters)
        {
            var posts = _context.Posts.OrderByDescending(x => x.CreateDateTime).AsQueryable();
            var likes = _context.PostsLiked.Where(x => x.UserId == userId);

            posts = likes.Select(x => x.ThePost);

            return await PagedList<PostsDTO>.CreateAsync(
                posts.ProjectTo<PostsDTO>(_mapper.ConfigurationProvider).AsNoTracking(),
                postParameters.PageIndex,
                postParameters.PageSize);
        }

        public async Task<IEnumerable<BaseMemberDTO>> GetUsersLikedPost(int postId)
        {
            var posts = _context.Posts.Where(x => x.Id == postId).Select(x => x.TheUser);
            var users = await posts.ProjectTo<BaseMemberDTO>(_mapper.ConfigurationProvider).ToListAsync();

            return users;
        }
    }
}
