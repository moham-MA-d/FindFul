using System.Collections.Generic;
using System.Threading.Tasks;
using API.Controllers.Version1.Base;
using API.Errors;
using API.Extensions;
using Core.IServices.Mapper;
using Core.IServices.Posts;
using DTO.Pagination;
using DTO.Posts;
using Extensions.Common;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Version1
{
    public class PostsController : BaseApiController
    {
        private readonly IPostService _postService;
        private readonly IMapperService _mapperService;

        public PostsController(IPostService postService, IMapperService mapperService)
        {
            _postService = postService;
            _mapperService = mapperService;
        }

        /// <summary>
        /// Create a post
        /// </summary>
        /// <response code="201">Post is created successfully</response>
        /// <response code="400">Post is not created</response>
        [ProducesResponseType(typeof(DtoPostResponse), 201)]
        [ProducesResponseType(typeof(ApiException), 400)]
        [HttpPost("Add")]
        public async Task<ActionResult<DtoPostResponse>> Add(DtoPostRequest dtoPostRequest)
        {
            var post = _mapperService.DtoPostRequestToPost(dtoPostRequest);
            post.UserId = User.GetUserId();
            await _postService.AddAsync(post);

            var dtoPostResponse = _mapperService.PostToDtoPostResponse(post);

            var baseUrl = HttpContext.GetCurrentLocationUri();

            var locationUri = baseUrl + "/" + post.Id;

            return Created(locationUri, dtoPostResponse);
        }


        


        /// <summary>
        /// Get all posts
        /// </summary>
        /// <response code="200">Get all posts</response>
        /// <response code="204">No posts found</response>
        [ProducesResponseType(typeof(IEnumerable<DtoPostResponse>), 200)]
        [ProducesResponseType(204)]
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<DtoPostResponse>>> GetAll([FromQuery] PostParameters postParameters)
        {
            ++postParameters.PageIndex;

            var userId = User.GetUserId();

            var posts = await _postService.GetAllPosts(postParameters, userId);

            if (posts == null) return NoContent();

            Response.AddPaginationHeader(posts.PageIndex, posts.PageSize, posts.TotalItems, posts.TotalPages);

            return Ok(posts);
        }


        /// <summary>
        /// Get a post
        /// </summary>
        /// <response code="200">Get a post</response>
        /// <response code="404">Post is not found</response>
        [ProducesResponseType(typeof(DtoPostResponse), 200)]
        [ProducesResponseType(typeof(ApiException), 404)]
        [HttpGet("{postId}")]
        public async Task<ActionResult<DtoPostResponse>> Get(int postId)
        {
            var post = await _postService.GetByIdAsync(postId);

            if (post == null) return NotFound();

            var dtoPostResponse = _mapperService.PostToDtoPostResponse(post);

            return Ok(dtoPostResponse);
        }




        /// <summary>
        /// Edit a post
        /// </summary>
        /// <response code="200">Post is edited successfully</response>
        /// <response code="400">Post is not edited</response>
        [ProducesResponseType(typeof(DtoPostResponse), 200)]
        [ProducesResponseType(typeof(ApiException), 400)]
        [HttpPut("Edit")]
        public async Task<ActionResult<DtoPostResponse>> Edit(DtoPostRequest dtoPostRequest)
        {
            var post = await _postService.GetByIdAsync(dtoPostRequest.Id);

            if (post == null) return BadRequest("The post that you are looking for it not found!");

            if (post.UserId != User.GetUserId()) return BadRequest("You cannot edit this post.");

            var updatedPost = _mapperService.DtoPostRequestToPost(dtoPostRequest);

            _postService.Update(updatedPost);

            var dtoPostResponse = _mapperService.PostToDtoPostResponse(updatedPost);

            return Ok(dtoPostResponse);
        }


        /// <summary>
        /// Delete a post
        /// </summary>
        /// <response code="200">Post is deleted successfully</response>
        /// <response code="400">Post is not deleted</response>
        [ProducesResponseType(typeof(DtoPostResponse), 200)]
        [ProducesResponseType(typeof(ApiException), 400)]
        [HttpDelete("Delete/{postId}")]
        public async Task<ActionResult<DtoPostResponse>> Delete(int postId)
        {
            var post = await _postService.GetByIdAsync(postId);

            if (post == null) return BadRequest("The post that you are looking for it not found!");

            if (post.UserId != User.GetUserId()) return BadRequest("You cannot delete this post.");

            post.IsDelete = true;

            _postService.Update(post);

            return Ok("Post is deleted!");
        }
    }
}
