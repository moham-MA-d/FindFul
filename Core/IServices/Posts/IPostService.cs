﻿using Core.Models.Entities.Posts;
using DTO.Pagination;
using DTO.Posts;
using System.Threading.Tasks;

namespace Core.IServices.Posts
{
    public interface IPostService : IEntityService<Post>
    {
        Task<PagedListBase<DtoPostResponse>> GetAllPosts(PostParameters postParameters, int? userId);
    }
}
