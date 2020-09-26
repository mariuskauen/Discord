using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.Core.Models
{
    public class ConversationVm
    {
        public ConversationVm()
        {
            users = new List<ConversationUsers>();
            messages = new List<ConversationMessage>();
        }

        public string id { get; set; }
        public List<ConversationUsers> users { get; set; }

        public List<ConversationMessage> messages { get; set; }
    }
}
