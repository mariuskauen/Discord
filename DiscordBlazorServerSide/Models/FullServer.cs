using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DiscordBlazorServerSide.Models
{
    public class FullServer
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("owner")]
        public string Owner { get; set; }

        //[JsonPropertyName("isOwner")]
        //public bool IsOwner { get; set; }

        [JsonPropertyName("users")]
        public List<ServerUserList> Users { get; set; }

        [JsonPropertyName("channels")]
        public List<ChannelVm> Channels { get; set; }
    }
}
