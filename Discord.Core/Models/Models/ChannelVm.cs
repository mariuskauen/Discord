using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Discord.Core.Models
{
    public class ChannelVm
    {
        public string Id { get; set; }

        public List<MessageVm> Messages { get; set; }

        public string Name { get; set; }

        public string ServerId { get; set; }
    }
}
