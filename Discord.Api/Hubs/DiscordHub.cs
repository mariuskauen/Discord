//using Discord.Api.Data;
//using Discord.Core.Helpers;
//using Discord.Core.Models;
//using Microsoft.AspNetCore.SignalR;
//using Microsoft.EntityFrameworkCore;
//using MongoDB.Driver;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace Discord.Hubs
//{
//    public class DiscordHub : Hub
//    {
//        private readonly DataContext _context;
//        private readonly MapConfig _mapConfig;
//        public DiscordHub(DataContext Context, MapConfig mapConfig)
//        {
//            _context = Context;
//            _mapConfig = mapConfig;
//        }
//        string userId;
//        //public override async Task OnConnectedAsync()
//        //{
//        //    userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

//        //    foreach (ServerUser serv in await _context.ServersUsers.Where(x => x.Id.Contains(userId)).ToListAsync())
//        //    {
//        //        string serverId = serv.Id.Split(':')[1];
//        //        await Groups.AddToGroupAsync(Context.ConnectionId, serverId);
//        //    }
//        //    foreach (Conversation con in await _context.Conversations.Where(x=>x.Userids.Contains(userId)).ToListAsync())
//        //    {
//        //        await Groups.AddToGroupAsync(Context.ConnectionId, con.Id);
//        //    }
//        //    await Groups.AddToGroupAsync(Context.ConnectionId, userId);
//        //}


//        public async Task SendMessage(MessageDTO messageDTO)
//        {
//            userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

//            User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

//            messageDTO.Id = Guid.NewGuid().ToString();
//            messageDTO.SenderId = userId;
//            messageDTO.SenderName = user.Username;

//            var chan = await _context.Channels.FirstOrDefaultAsync(x => x.Id == messageDTO.BelongsTo);

//            if (chan == null)
//                return;

//            messageDTO.ServerId = chan.ServerId;

//            var mapper = _mapConfig.MessageDTOToMessage.CreateMapper();
//            Message message = mapper.Map<Message>(messageDTO);

//            MongoClient mClient = new MongoClient();
//            IMongoDatabase db = mClient.GetDatabase("DiscordMessages");
//            var collection = db.GetCollection<MongoMessages>("Messages");

//            var filter = Builders<MongoMessages>.Filter.Eq("_id", message.BelongsTo);

//            var record = await collection.Find(filter).FirstOrDefaultAsync();
//            if (record != null)
//            {
//                var update = Builders<MongoMessages>.Update.Push("Messages", message);
//                await collection.FindOneAndUpdateAsync(filter, update);
//            }
//            else
//            {
//                MongoMessages mMess = new MongoMessages()
//                {
//                    Id = message.BelongsTo
//                };
//                mMess.Messages.Add(message);

//                await collection.InsertOneAsync(mMess);
//            }

//            await Clients.Group(messageDTO.ServerId).SendAsync("Message", messageDTO);

//        }
//        //public async Task SendPrivateMessage(NewPrivateMessageDTO message)
//        //{
//        //    userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//        //    User user = await _context.Users
//        //        .Include(a => a.Conversations)
//        //        .ThenInclude(b => b.Conversation)
//        //        .ThenInclude(c => c.Messages)
//        //        .FirstOrDefaultAsync(x => x.Id == userId);

//        //    ConversationList con = user.Conversations.FirstOrDefault(x => x.Conversation.Id == message.ConversationId);


//        //    message.Id = Guid.NewGuid().ToString();

//        //    var mapper = _mapConfig.PrivateMessageFromDTO.CreateMapper();

//        //    con.Conversation.Messages.Add(mapper.Map<Message>(message));
//        //    con.LastSeen = DateTime.Now;
//        //    await _context.SaveChangesAsync();

//        //    await Clients.Group(message.ConversationId).SendAsync("PrivateMessage", message);
//        //}

//        //public async Task ConversationSeen(string conversationId)
//        //{
//        //    userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//        //    User user = await _context.Users
//        //        .Include(a => a.Conversations)
//        //        .ThenInclude(b => b.Conversation)
//        //        .ThenInclude(c => c.Messages)
//        //        .FirstOrDefaultAsync(x => x.Id == userId);



//        //    ConversationList con = user.Conversations.FirstOrDefault(x => x.ConversationId == conversationId);

//        //    con.LastSeen = DateTime.Now;
//        //    await _context.SaveChangesAsync();
//        //}
//    }
//}
