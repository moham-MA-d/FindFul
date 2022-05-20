using System.Collections.Generic;
using System.Threading.Tasks;
using API.Controllers.Version1.Base;
using API.Extensions;
using Core.IServices.Mapper;
using Core.IServices.Posts;
using DTO.Pagination;
using DTO.Posts;
using Extensions.Common;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Version1
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
        public async Task<ActionResult<DtoPostResponse>> AddPost(DtoPostRequest dtoPostRequest)
        {
            var post = _mapperService.DtoPostRequestToPost(dtoPostRequest);
            post.UserId = User.GetUserId();
            await _postService.AddAsync(post);

            var dtoPostResponse = _mapperService.PostToDtoPostResponse(post);
            return Ok(dtoPostResponse);
        }

        [HttpGet("GetPosts")]
        public async Task<ActionResult<IEnumerable<DtoPostResponse>>> GetPosts([FromQuery] PostParameters postParameters)
        {
            ++postParameters.PageIndex;

            var userId = User.GetUserId();

            var posts = await _postService.GetAllPosts(postParameters, userId);

            Response.AddPaginationHeader(posts.PageIndex, posts.PageSize, posts.TotalItems, posts.TotalPages);

            return Ok(posts);
        }
    }
}
