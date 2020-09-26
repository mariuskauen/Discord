using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Discord.Core.Models
{
    public class Channel : Base
    {
        public string Name { get; set; }

        public string ServerId { get; set; }
    }
}
