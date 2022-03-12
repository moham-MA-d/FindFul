using DTO.Account;
using System.Linq;
using AutoMapper;
using Core.Models.Entities.User;
using Extentions.Common;
using DTO.Account.Photo;

namespace Service.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDTO>()
                .ForMember(dst => dst.PhotoUrl, opt => opt.MapFrom(src => src.TheUserPhotosList.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dst => dst.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<UserPhoto, MemberPhotoDTO>();
            CreateMap<MemberUpdateDTO, AppUser>();
        }
    }
}
