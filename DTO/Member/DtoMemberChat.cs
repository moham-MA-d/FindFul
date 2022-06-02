using DTO.Account.Base;
using System;

namespace DTO.Member
{
    public class DtoMemberChat : DtoBaseMember
    {
        public string LastMessage { get; set; }
        public DateTime LastMessageDateTime { get; set; }
    }
}
