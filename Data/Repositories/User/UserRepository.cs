using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Core.Models.Entities.User;
using Data;
using DTO.Account;
using Data.Repositories;
using Core.IRepositories.User;

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
        public async Task<MemberDTO> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.UserName == username);
        }
        public async Task<IEnumerable<MemberDTO>> GetAllMembers()
        {
            try
            {
                return await _context.Users
                    .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(UserRepository));
                return new List<MemberDTO>();
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