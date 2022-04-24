using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.IRepositories.Posts;
using Core.Models.Entities.Posts;
using Data.Helpers;
using DTO._Enumarations;
using DTO.Pagination;
using DTO.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories.Posts
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        private readonly IMapper _mapper;

        public PostRepository(DataContext context, ILogger logger, IMapper mapper) : base(context, logger)
        {
            _mapper = mapper;
        }

        public async Task<PagedListBase<PostsDTO>> GetAllPosts(PostParameters postParameters)
        {
            var query = _context.Posts.AsQueryable();


            query = postParameters.OrderBy switch
            {
                PostEnums.OrderBy.Newest => query.OrderByDescending(x => x.CreateDateTime),
                PostEnums.OrderBy.Oldest => query.OrderBy(x => x.CreateDateTime),
                _ => query.OrderByDescending(x => x.CreateDateTime),
            };

            return await PagedList<PostsDTO>.CreateAsync(query.ProjectTo<PostsDTO>(_mapper.ConfigurationProvider).AsNoTracking(),
                postParameters.PageIndex,
                postParameters.PageSize);

        }
    }
}
