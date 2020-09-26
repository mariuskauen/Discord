using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.Core.Models
{
    public class ConversationDTO
    {
        public ConversationDTO()
        {
            Messages = new List<MessageDTO>();
        }
        public string Id { get; set; }

        public string Userlist { get; set; }

        public List<MessageDTO> Messages { get; set; }
    }
}
