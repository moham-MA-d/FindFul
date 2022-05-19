using Core;
using Core.IRepositories.User;
using Core.IServices.User;
using Core.Models.Entities.User;
using DTO.Account.Photo;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.IServices;

namespace Service.Classes.User
{
    public class UserPhotoService : EntityService<UserPhoto>, IUserPhotoService, IEntityService<UserPhoto>
    {
        private readonly IUserPhotoRepository _userPhotoRepository;

        public UserPhotoService(IUnitOfWork unitOfWork, IUserPhotoRepository userPhotoRepository) : base(unitOfWork, userPhotoRepository)
        {
            _userPhotoRepository = userPhotoRepository;
        }

        public async Task<IEnumerable<MemberPhotoDTO>> GetAllUserPhotos(int userId)
        {
            return await _userPhotoRepository.GetAllUserPhotos(userId);
        }
    }
}
