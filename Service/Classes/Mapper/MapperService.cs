using AutoMapper;
using Core.IServices.Mapper;
using Core.Models.Entities.Messages;
using Core.Models.Entities.Posts;
using Core.Models.Entities.User;
using DTO.Account;
using DTO.Account.Photo;
using DTO.Messages;
using DTO.Posts;

namespace Service.Classes.Mapper
{
    public class MapperService : IMapperService
    {
        private readonly IMapper _mapper;

        public MapperService(IMapper mapper)
        {
            _mapper = mapper;
        }


        public AppUser DtoMemberUpdateToAppUser(DtoMemberUpdate dtoMemberUpdate, AppUser appUser)
        {
            return _mapper.Map(dtoMemberUpdate, appUser);
        }
        public MemberPhotoDTO UserPhotoToDtoMemberPhoto(UserPhoto userPhoto)
        {
            return _mapper.Map<MemberPhotoDTO>(userPhoto);
        }
        
        public Post DtoPostRequestToPost(DtoPostRequest dtoPostRequest)
        {
            return _mapper.Map<Post>(dtoPostRequest);
        }

        public DtoPostResponse PostToDtoPostResponse(Post post)
        {
            return _mapper.Map<DtoPostResponse>(post);
        }

        public DtoMessageResponse MessageToDtoMessage(Message message)
        {
            return _mapper.Map<DtoMessageResponse>(message);
        }
    }
}
