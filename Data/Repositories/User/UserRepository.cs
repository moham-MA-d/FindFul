using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Core.Models.Entities.User;
using DTO.Account;
using Core.IRepositories.User;
using System.Linq;
using DTO.Pagination;
using Data.Helpers;

namespace Data.Repositories.User
{
    public class UserRepository : GenericRepository<AppUser>, IUserRepository
    {
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, ILogger logger, IMapper mapper) : base(context, logger)
        {
            _mapper = mapper;
        }


        public async Task<MemberDTO> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Email == email);
        }
        public MemberDTO GetUserByUsername(string username)
        {
            return _context.Users
                .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefault(x => x.UserName == username);
        }
        public async Task<MemberDTO> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.UserName == username);
        }
        public async Task<PagedListBase<MemberDTO>> GetAllMembers(PageParameters pageParameters)
        {
            try
            {
                var query = _context.Users
                    .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                    
                    //Because We only going to read the data and we are not going to to anything else with the data
                    .AsNoTracking();

                return await PagedList<MemberDTO>.CreateAsync(query, pageParameters.PageIndex, pageParameters.PageSize);
                    
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(UserRepository));
                return null;
            }
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(x => x.TheUserPhotosList)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        //public async Task<int> Update(AppUser entity)
        //{
        //    var catalogue = await _context.Users.FindAsync(entity);
        //    this._context.Entry(catalogue).CurrentValues.SetValues(entity);
        //    return 1;
        //}

        //public override async Task<bool> Update(AppUser entity)
        //{
        //    try
        //    {
        //        var existingUser = await _dbSet.Where(x => x.Id == entity.Id)
        //                                            .FirstOrDefaultAsync();

        //        if (existingUser == null)
        //            return await Add(entity);

        //        existingUser.FirstName = entity.FirstName;
        //        existingUser.LastName = entity.LastName;
        //        existingUser.Email = entity.Email;

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "{Repo} Upsert function error", typeof(UserRepository));
        //        return false;
        //    }
        //}
    }
}