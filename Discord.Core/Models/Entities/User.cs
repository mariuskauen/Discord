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

        public string ProfilePicture { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public Status Status { get; set; }

        public DateTime LastOnline { get; set; }

        public DateTime DateJoined { get; set; }

        public UserSettings Settings { get; set; }

        public List<string> FriendShips { get; set; }

        public List<string> FriendRequests { get; set; }

        public List<string> Conversations { get; set; }

        public List<string> Servers { get; set; }

    }



}
