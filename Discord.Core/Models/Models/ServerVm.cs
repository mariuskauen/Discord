using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.Core.Models.Models
{
    public class ServerVm
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string OwnerId { get; set; }

        public List<ChannelVm> Channels { get; set; }

        public List<UserVm> Users { get; set; }
    }
}
