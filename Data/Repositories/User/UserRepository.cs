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
using DTO._Enumarations;

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
        public async Task<MemberDTO> GetMemberByUsernameAsync(string username)
        {
            return await _context.Users
                .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.UserName == username);
        }
        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .SingleOrDefaultAsync(x => x.UserName == username);
        }
        public async Task<PagedListBase<MemberDTO>> GetAllMembers(UserParameters userParameters)
        {
            try
            {
                var query = _context.Users.AsQueryable();

                query = query.Where(x => x.UserName != userParameters.CurrentUsername);

                if (userParameters.Sex != UserEnums.Sex.All)
                    query = query.Where(x => x.Sex == (int)userParameters.Sex);

                if (userParameters.Gender != UserEnums.Gender.All)
                    query = query.Where(x => x.Gender == (int)userParameters.Gender);

                var minDob = DateTime.Today.AddYears(-userParameters.MaxAge - 1);
                var maxDob = DateTime.Today.AddYears(-userParameters.MinAge);
                query = query.Where(x => x.DateOfBirth >= minDob && x.DateOfBirth <= maxDob);

                query = userParameters.OrderBy switch
                {
                    UserEnums.OrderBy.Newest => query.OrderByDescending(x => x.CreateDateTime),
                    UserEnums.OrderBy.Oldest => query.OrderBy(x => x.CreateDateTime),
                    UserEnums.OrderBy.AgeAsc => query.OrderByDescending(x => x.DateOfBirth),
                    UserEnums.OrderBy.AgeDes => query.OrderBy(x => x.DateOfBirth),
                    UserEnums.OrderBy.LastActivity => query.OrderByDescending(x => x.LastActivity),
                    _ => query.OrderByDescending(x => x.CreateDateTime),
                };


                //We used `AsNoTracking()` Because We only going to read the data and we are not going to to anything else with the data
                return await PagedList<MemberDTO>.CreateAsync(
                    query.ProjectTo<MemberDTO>(_mapper.ConfigurationProvider, new {currentUsername = userParameters.CurrentUsername}).AsNoTracking(),
                    userParameters.PageIndex,
                    userParameters.PageSize);

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

        public async Task<AppUser> GetUserByIdAsync(int id, IQueryable<AppUser> query)
        {
            var result = await query.FirstOrDefaultAsync(x => x.Id == id);

            var ss = result.TheFollowerList.Select(x => x.TheFollower);

            return result;
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