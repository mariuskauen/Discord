using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Discord.Core.Models
{
    public class ConversationList
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("conversation")]
        public ConversationVm Conversation { get; set; }

        [JsonPropertyName("lastSeen")]
        public DateTime LastSeen { get; set; }
    }
}
