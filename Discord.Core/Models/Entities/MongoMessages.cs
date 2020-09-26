using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.Core.Models
{
    public class MongoMessages
    {
        public MongoMessages()
        {
            Messages = new List<Message>();
        }
        public string Id { get; set; }

        public List<Message> Messages { get; set; }
    }
}
