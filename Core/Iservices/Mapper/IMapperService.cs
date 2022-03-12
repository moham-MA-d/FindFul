using Core.Models.Entities.User;
using DTO.Account;
using DTO.Account.Photo;

namespace Core.IServices.Mapper
{
    public interface IMapperService
    {
        AppUser MemberUpdateDtoToAppUser(MemberUpdateDTO memberUpdateDTO, AppUser appUser);
        MemberPhotoDTO UserPhotoToMemberPhotoDto(UserPhoto userPhoto);
    }
}
