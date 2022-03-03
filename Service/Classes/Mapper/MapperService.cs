using AutoMapper;
using Core.Iservices.Mapper;
using Core.Models.Entities.User;
using DTO.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
