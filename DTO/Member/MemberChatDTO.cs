using DTO.Account.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Member
{
    public class MemberChatDTO : BaseMemberDTO
    {
        public string LastMessage { get; set; }
        public DateTime LastMessageDateTime { get; set; }
    }
}
