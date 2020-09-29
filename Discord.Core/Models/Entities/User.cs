using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discord.Core.Models
{
    public class User : Base
   {
        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public Status Status { get; set; }

        public string Conversations { get; set; }

        public string Servers { get; set; }

        public string FriendShips { get; set; }

        public string FriendRequests { get; set; }

        public string Settings { get; set; }

        public string ProfilePicture { get; set; }

        public DateTime DateJoined { get; set; }

        public DateTime LastOnline { get; set; }
    }
}
