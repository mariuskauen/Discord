using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.Core.Models
{
    public class Home
    {
        public Home()
        {
            friends = new List<FriendList>();
            myrequests = new List<FriendRequestVm>();
            othersrequests = new List<FriendRequestVm>();
            Conversations = new List<ConversationList>();
        }
        public FullUserDTO user { get; set; }

        public List<FriendList> friends { get; set; }

        public List<FriendRequestVm> myrequests { get; set; }

        public List<FriendRequestVm> othersrequests { get; set; }

        public List<ConversationList> Conversations { get; set; }
    }
}
