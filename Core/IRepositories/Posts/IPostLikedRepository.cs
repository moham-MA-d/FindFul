﻿using Core.Models.Entities.Posts;
using DTO.Account.Base;
using DTO.Pagination;
using DTO.Posts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.IRepositories.Posts
{
	public interface IPostLikedRepository : IGenericRepository<PostLiked>
	{
		Task<PostLiked> GetPostLike(int postId, int userId);
		Task<IEnumerable<DtoPostRequest>> GetPostsLikedByUser(int userId, PostParameters postParameters);
		Task<IEnumerable<DtoBaseMember>> GetUsersLikedPost(int postId);


	}
}
