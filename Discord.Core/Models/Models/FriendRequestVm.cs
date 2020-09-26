using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Discord.Core.Models
{
    public class FriendRequestVm
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("usersId")]
        public string UsersId { get; set; }
    }
}
