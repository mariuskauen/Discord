using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.Core.Models
{
    public class FriendListDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public bool Online { get; set; }
    }
}
