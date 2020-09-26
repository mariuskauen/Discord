using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discord.Core.Models
{
    public class Server : Base
    {
        public string Name { get; set; }

        public string OwnerId { get; set; }
    }
}
