using AutoMapper;
using DTO.Account;
using DTO.Media;
using Core.Models.Entities.User;
using System.Linq;
using Extentions.Common;

namespace Data.Helpers
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
