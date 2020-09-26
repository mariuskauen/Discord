using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.Core.Models
{
    public class ServerDTO
    {
        public ServerDTO()
        {
            Channels = new List<ChannelDTO>();
            Users = new List<ServerUserListDTO>();
        }
        public string Id { get; set; }

        public string Name { get; set; }

        public string OwnerId { get; set; }

        public List<ChannelDTO> Channels { get; set; }

        public List<ServerUserListDTO> Users { get; set; }
    }
}
