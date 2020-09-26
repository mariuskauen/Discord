using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discord.Core.Models
{
    public class Message : Base
    {
        public string SenderName { get; set; }

        public string SenderId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Content { get; set; }

        public string BelongsTo { get; set; }
    }
}
