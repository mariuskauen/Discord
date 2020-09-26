using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.Core.Models
{
    public class ConversationMessage
    {
        public string id { get; set; }

        public string username { get; set; }

        public string content { get; set; }

        public DateTime timeCreated { get; set; }

        public bool Seen { get; set; }
    }
}
