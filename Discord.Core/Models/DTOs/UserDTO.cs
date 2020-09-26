using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.Core.Models
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public bool Online { get; set; }
    }
}
