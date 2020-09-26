using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.Core.Models
{
    public class FriendRequestDTO
    {
        public string Id { get; set; }

        public SmallUserDTO User { get; set; }

        public bool IsSenderMe { get; set; }
    }
}
