using DTO.Account;
using DTO.Media;
using System.Linq;
using AutoMapper;
using Core.Models.Entities.User;
using Extentions.Common;

namespace Service.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDTO>()
                .ForMember(dst => dst.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dst => dst.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<UserPhoto, PhotoDTO>();
        }
    }
}
