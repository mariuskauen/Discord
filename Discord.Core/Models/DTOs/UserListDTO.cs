using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.Core.Models.DTOs
{
    public class UserListDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }

        public bool Online { get; set; }
    }
}
