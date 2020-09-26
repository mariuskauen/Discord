using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBlazorServerSide.Models
{
    public class FirstLoad
    {
        public FirstLoad()
        {
            home = new Home();
            servers = new List<ServerList>();
        }
        public Home home { get; set; }

        public List<ServerList> servers { get; set; }
    }
}
