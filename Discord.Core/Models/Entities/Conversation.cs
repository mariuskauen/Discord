﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discord.Core.Models
{
    public class Conversation : Base
    {
        public string Userlist { get; set; }

        public string Userids { get; set; }
    }
}
