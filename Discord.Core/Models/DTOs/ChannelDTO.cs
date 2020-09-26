using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.Core.Models
{
    public class ChannelDTO
    {
        public ChannelDTO()
        {
            Messages = new List<MessageDTO>();
        }
        public string Id { get; set; }

        public string Name { get; set; }

        public string ServerId { get; set; }

        public List<MessageDTO> Messages { get; set; }
    }
}
