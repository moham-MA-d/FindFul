using Core.Models.Entities.User;
using DTO.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Iservices.Mapper
{
    public interface IMapperService
    {
        AppUser MemberUpdateDtoToAppUser(MemberUpdateDTO memberUpdateDTO, AppUser appUser);
    }
}
