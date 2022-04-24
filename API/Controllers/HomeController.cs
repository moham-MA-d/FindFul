using API.Controllers.Base;
using Core.IServices.Mapper;
using Core.IServices.Posts;
using DTO.Pagination;
using DTO.Posts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class HomeController : BaseApiController
    {
        private readonly IPostService _postService;
        private readonly IMapperService _mapperService;

        public HomeController(IPostService postService, IMapperService mapperService)
        {
            _postService = postService;
            _mapperService = mapperService;
        }  


        [HttpPost("AddPost")]
        public async Task<IActionResult> AddPost(PostsDTO postDto)
        {
            var post = _mapperService.PostsDtoToPost(postDto);
            await _postService.AddAsync(post);

            return Ok(post);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostsDTO>>> GetPosts([FromQuery] PostParameters postParameters)
        {
            var p = await _postService.GetAllPosts(postParameters);
            return null;
        }
    }
}
