﻿using System.Threading.Tasks;
using API.Controllers.Version1.Base;
using API.Helpers.Authentication;
using Core.IServices.Posts;
using Core.Models.Entities.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Version1
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

            if (post.UserId == userId) return BadRequest("You cannot like your post!");

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
                    UserId = userId,
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
