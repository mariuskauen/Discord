using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.Core.Models
{
    public class FirstLoadDTO
    {
        public FirstLoadDTO()
        {
            Servers = new List<ServerListDTO>();
            Requests = new List<FriendRequestDTO>();
            Friends = new List<FriendListDTO>();
            Conversations = new List<ConversationListDTO>();

        }
        public UserDTO User { get; set; }

        public List<ServerListDTO> Servers { get; set; }

        public List<FriendRequestDTO> Requests { get; set; }

        public List<FriendListDTO> Friends { get; set; }

        public List<ConversationListDTO> Conversations { get; set; }
    }
}
