using Core.Models.Entities.Messages;
using Core.Models.Entities.Posts;
using Core.Models.Entities.User;
using DTO.Account;
using DTO.Account.Photo;
using DTO.Messages;
using DTO.Posts;

namespace Core.IServices.Mapper
{
    public interface IMapperService
    {
        AppUser DtoMemberUpdateToAppUser(DtoMemberUpdate dtoMemberUpdate, AppUser appUser);
        DtoMemberPhoto UserPhotoToDtoMemberPhoto(UserPhoto userPhoto);

        Post DtoPostRequestToPost(DtoPostRequest dtoPostRequest);
        DtoPostResponse PostToDtoPostResponse(Post post); 

        DtoMessageResponse MessageToDtoMessage(Message message);
    }
}
