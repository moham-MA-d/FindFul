using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.IRepositories.User;
using Core.Models.Entities.User;
using DTO.Account.Photo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories.User
{
    public class UserPhotoRepository : GenericRepository<UserPhoto>, IUserPhotoRepository
    {
        private readonly IMapper _mapper;

        public UserPhotoRepository(DataContext context, ILogger ilogger, IMapper mapper) : base(context, ilogger)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<MemberPhotoDTO>> GetAllUserPhotos(int userId)
        {
            return await _context.UserPhotos.Where(x => x.AppUserId == userId)
                .ProjectTo<MemberPhotoDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
