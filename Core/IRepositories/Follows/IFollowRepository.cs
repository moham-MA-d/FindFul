﻿using Core.Models.Entities.Follows;
using DTO.Account;
using DTO.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.IRepositories.Follows
{
	public interface IFollowRepository : IGenericRepository<Follow>
	{
		//check if a User has already followed a particular User.
		Task<Follow> GetFollow(int userId, int recipientId);

		Task<PagedListBase<MemberDTO>> GetFollowing(UserParameters userParameters);
		Task<PagedListBase<MemberDTO>> GetFollowers(UserParameters userParameters);


	}
}