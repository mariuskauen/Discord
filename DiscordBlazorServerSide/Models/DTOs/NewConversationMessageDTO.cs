using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBlazorServerSide.Models
{
    public class NewConversationMessageDTO
    {
        public string Id { get; set; }

        public string SenderName { get; set; }

        public string SenderId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Content { get; set; }

        public string ConversationId { get; set; }
    }
}
