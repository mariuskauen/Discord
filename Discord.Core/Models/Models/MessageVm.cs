using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Discord.Core.Models
{
    public class MessageVm
    {
        public string Id { get; set; }

        public string SenderName { get; set; }

        public string SenderId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Content { get; set; }

        public string BelongsTo { get; set; }
    }
}
