using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DiscordBlazorServerSide.Models
{
    public class FriendRequestVm
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("senderId")]
        public string SenderId { get; set; }

        [JsonPropertyName("receiverId")]
        public string ReceiverId { get; set; }
    }
}
