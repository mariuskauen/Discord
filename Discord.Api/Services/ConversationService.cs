using Discord.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discord.Api.Services
{
    public class ConversationService
    {
        public async Task<string> CreateNewConversationObject(string userId)
        {
            Conversation con = new Conversation() { Id = Guid.NewGuid().ToString() };

            return con.Id;
        }
    }
}
