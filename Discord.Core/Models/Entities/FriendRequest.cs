using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Discord.Core.Models
{
    public class FriendRequest : Base
    {
        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public bool IsActive { get; set; }
    }
}
