using Core.Models.Entities.User;
using DTO.Account.Photo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.IServices.User
{
    public interface IUserPhotoService : IEntityService<UserPhoto>
    {
        Task<IEnumerable<MemberPhotoDTO>> GetAllUserPhotos(int userId);
    }
}
