using API.Controllers.Base;
using API.Extensions;
using Core.IService.User;
using Core.IServices.Posts;
using Core.Models.Entities.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    public class LikeController : BaseApiController
    {
        private readonly IPostService _postService;
        private readonly IPostedLikedService _postedLikedService;

        public LikeController(IPostService postService, IPostedLikedService postedLikedService)
        {
            _postService = postService;
            _postedLikedService = postedLikedService;
        }


        [HttpPost("{postId}")]
        public async Task<ActionResult> AddPostLike(int postId)
        {
            var userId = User.GetUserId();
            var post = await _postService.GetByIdAsync(postId);

            if (post == null) return NotFound();

            if (post.AppUserId == userId) return BadRequest("You cannot like your post!");

            var postLiked = await _postedLikedService.GetPostLike(postId, userId);

            if (postLiked != null)
            {
                if (postLiked.IsActive == true)
                {
                    postLiked.IsActive = false;
                    _postedLikedService.Update(postLiked);

                    --post.LikesCount;
                    _postService.Update(post);

                    return Ok(new { data = "unlike" });
                }
                else
                {
                    postLiked.IsActive = true;

                    ++post.LikesCount;
                    _postService.Update(post);
                    _postedLikedService.Update(postLiked);

                    return Ok(new { data = "like" });
                }
            }
            else
            {
                postLiked = new PostLiked
                {
                    PostId = postId,
                    AppUserId = userId,
                    IsActive = true,
                };
                await _postedLikedService.AddAsync(postLiked);

                ++post.LikesCount;
                _postService.Update(post);

                return Ok(new { data = "like" });
            }
        }

    } 
}
