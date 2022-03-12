﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Entities.Tag
{
    public class Tag : BaseId
    {
        public string Name { get; set; }
        public DateTime CreateDateTime { get; set; }
        public Int64 FirstUserId { get; set; }

        public int Count { get; set; }
    }
}
