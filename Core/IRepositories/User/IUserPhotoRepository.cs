using Core.Models.Entities.User;
using DTO.Account.Photo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.IRepositories.User
{
    public interface IUserPhotoRepository : IGenericRepository<UserPhoto>
    {
        Task<IEnumerable<DtoMemberPhoto>> GetAllUserPhotos(int userId);
    }
}
