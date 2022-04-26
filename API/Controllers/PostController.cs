using API.Controllers.Base;
using API.Extensions;
using Core.IServices.Mapper;
using Core.IServices.Posts;
using DTO.Pagination;
using DTO.Posts;
using Extensions.Common;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class PostController : BaseApiController
    {
        private readonly IPostService _postService;
        private readonly IMapperService _mapperService;

        public PostController(IPostService postService, IMapperService mapperService)
        {
            _postService = postService;
            _mapperService = mapperService;
        }


        [HttpPost("AddPost")]
        public async Task<ActionResult<PostsDTO>> AddPost(PostsDTO postDto)
        {
            var post = _mapperService.PostsDtoToPost(postDto);
            post.AppUserId = User.GetUserId();
            await _postService.AddAsync(post);

            return Ok(postDto);
        }

        [HttpGet("GetPosts")]
        public async Task<ActionResult<IEnumerable<PostsDTO>>> GetPosts([FromQuery] PostParameters postParameters)
        {
            ++postParameters.PageIndex;

            var posts = await _postService.GetAllPosts(postParameters);

            Response.AddPaginationHeader(posts.PageIndex, posts.PageSize, posts.TotalItems, posts.TotalPages);

            return Ok(posts);
        }
    }
}
