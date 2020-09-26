using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.Core.Models.Models
{
    public class UserVm
    {
        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public bool Online { get; set; }
    }
}
