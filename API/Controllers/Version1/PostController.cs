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


        [HttpPost("Add")]
        public async Task<ActionResult<DtoPostResponse>> Add(DtoPostRequest dtoPostRequest)
        {
            var post = _mapperService.DtoPostRequestToPost(dtoPostRequest);
            post.UserId = User.GetUserId();
            await _postService.AddAsync(post);

            var dtoPostResponse = _mapperService.PostToDtoPostResponse(post);
            return Ok(dtoPostResponse);
        }


        [HttpGet("Get/{postId}")]
        public async Task<ActionResult<IEnumerable<DtoPostResponse>>> Get(int postId)
        {
            var post = await _postService.GetByIdAsync(postId);

            return Ok(post);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<DtoPostResponse>>> GetAll([FromQuery] PostParameters postParameters)
        {
            ++postParameters.PageIndex;

            var userId = User.GetUserId();

            var posts = await _postService.GetAllPosts(postParameters, userId);

            Response.AddPaginationHeader(posts.PageIndex, posts.PageSize, posts.TotalItems, posts.TotalPages);

            return Ok(posts);
        }
    }
}
