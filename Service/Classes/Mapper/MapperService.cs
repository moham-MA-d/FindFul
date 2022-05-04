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


        public AppUser MemberUpdateDtoToAppUser(MemberUpdateDTO memberUpdateDTO, AppUser appUser)
        {
            return _mapper.Map(memberUpdateDTO, appUser);
        }
        public MemberPhotoDTO UserPhotoToMemberPhotoDto(UserPhoto userPhoto)
        {
            return _mapper.Map<MemberPhotoDTO>(userPhoto);
        }
        
        public Post PostsDtoToPost(PostsDTO postsDTO)
        {
            return _mapper.Map<Post>(postsDTO);
        }

        public MessageDTO MessageToMessageDTO(Message message)
        {
            return _mapper.Map<MessageDTO>(message);
        }
    }
}
