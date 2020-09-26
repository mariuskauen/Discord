using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.Core.Models
{
    public class FirstLoad
    {
        public Home home { get; set; }

        public List<ServerList> servers { get; set; }
    }
}
