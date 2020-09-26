using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBlazorServerSide.Models
{
    public class Home
    {
        public Home()
        {
            friends = new List<FriendList>();
            requests = new List<FriendRequestVm>();
            conversations = new List<ConversationList>();
            servers = new List<ServerList>();
        }
        public FullUser user { get; set; }

        public List<FriendList> friends { get; set; }

        public List<FriendRequestVm> requests { get; set; }

        public List<ServerList> servers { get; set; }

        public List<ConversationList> conversations { get; set; }
    }
}
