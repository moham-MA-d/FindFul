using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Messages
{
    public class CreateMessageDTO
    {
        public string Body { get; set; }
        public string RecieverUsername { get; set; }
    }
}
