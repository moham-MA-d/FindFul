﻿using AutoMapper;
using Core.IServices.Mapper;
using Core.Models.Entities.User;
using DTO.Account;
using DTO.Account.Photo;

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
    }
}