//using Discord.Api.Data;
//using Discord.Core.Helpers;
//using Discord.Core.Models;
//using Microsoft.EntityFrameworkCore;
//using MongoDB.Driver;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Discord.Api.Services
//{
//    public class LoadService
//    {
//        private readonly DataContext context;
//        private readonly MapConfig mapConfig;

//        public LoadService(DataContext context, MapConfig mapConfig)
//        {
//            this.context = context;
//            this.mapConfig = mapConfig;
//            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
//        }
//        public async Task<FirstLoadDTO> FirstLoad(string userId)
//        {
//            FirstLoadDTO dto = new FirstLoadDTO();

//            //GET USER
//            var mapper = mapConfig.UserToUserDTO.CreateMapper();
//            dto.User = mapper.Map<UserDTO>(await context.Users.FirstOrDefaultAsync(x => x.Id == userId));

//            //GET SERVERLIST
//            mapper = mapConfig.ServerToServerListDTO.CreateMapper();
//            foreach (ServerUser su in await context.ServersUsers.Where(x => x.Id.Contains(userId)).ToListAsync())
//            {
//                string[] serverId = su.Id.Split(':');
//                Server s = await context.Servers.FirstOrDefaultAsync(x => x.Id == serverId[1]);
//                dto.Servers.Add(mapper.Map<ServerListDTO>(s));
//            }

//            //GET REQUESTLIST      
//            mapper = mapConfig.UserToSmallUserDTO.CreateMapper();
//            foreach (FriendRequest req in await context.Requests.Where(x => x.Id.Contains(userId) && x.IsActive == true).ToListAsync())
//            {
//                FriendRequestDTO frd = new FriendRequestDTO();
//                if (req.SenderId == userId)
//                {
//                    frd.Id = req.Id;
//                    frd.IsSenderMe = true;
//                    frd.User = mapper.Map<SmallUserDTO>(await context.Users.FirstOrDefaultAsync(x => x.Id == req.ReceiverId));
//                }
//                else
//                {
//                    frd.Id = req.Id;
//                    frd.IsSenderMe = false;
//                    frd.User = mapper.Map<SmallUserDTO>(await context.Users.FirstOrDefaultAsync(x => x.Id == req.SenderId));
//                }
//                dto.Requests.Add(frd);
//            }

//            //GET FRIENDLIST
//            mapper = mapConfig.FriendListToDto.CreateMapper();
//            foreach (FriendShip f in await context.Friends.Where(x => x.Id.Contains(userId) && x.IsActive == true).ToListAsync())
//            {
//                string friendId = "";
//                string[] id = f.Id.Split(':');
//                foreach (string str in id)
//                {
//                    if (str != userId)
//                        friendId = str;
//                }
//                dto.Friends.Add(mapper.Map<FriendListDTO>(await context.Users.FirstOrDefaultAsync(x => x.Id == friendId)));
//            }

//            //GET CONVERSATIONLIST
//            //foreach (Conversation c in await context.Conversations.Where(x => x.Userids.Contains(userId)).ToListAsync())
//            //{
//            //    ConversationListDTO cDto = new ConversationListDTO()
//            //    {
//            //        Id = c.Id,
//            //        Userlist = c.Userlist,
//            //        Userids = c.Userids
//            //    };

//            //    dto.Conversations.Add(cDto);
//            //}

//            //RETURN FINISHED DTO
//            return dto;
//        }

//        public async Task<ServerDTO> LoadServer(string serverId)
//        {

//            //GET BASIC SERVERSTUFF
//            var mapper = mapConfig.ServerToServerDTO.CreateMapper();
//            ServerDTO server = mapper.Map<ServerDTO>(await context.Servers.FirstOrDefaultAsync(x => x.Id == serverId));

//            //GET CHANNELS AND ADD MESSAGES TO FIRST CHANNEL
//            int i = 0;
//            foreach (Channel ch in await context.Channels.Where(x => x.ServerId == serverId).OrderBy(x => x.Name).ToListAsync())
//            {
//                mapper = mapConfig.ChannelToChannelDTO.CreateMapper();
//                ChannelDTO dto = mapper.Map<ChannelDTO>(ch);

//                if (i == 0)
//                {
//                    MongoClient mClient = new MongoClient();
//                    IMongoDatabase db = mClient.GetDatabase("discord");
//                    var collection = db.GetCollection<MongoMessages>("DiscordMessages");
//                    var filter = Builders<MongoMessages>.Filter.Eq("_id", ch.Id);
//                    MongoMessages mMessages = await collection.Find(filter).FirstOrDefaultAsync();
//                    if (mMessages != null)
//                    {
//                        mapper = mapConfig.MessageToMessageDTO.CreateMapper();
//                        if (mMessages.Messages.Count > 0)
//                        {
//                            foreach (Message mess in mMessages.Messages.OrderBy(x => x.CreatedAt).Take(100).ToList())
//                            {
//                                mess.CreatedAt = mess.CreatedAt.AddHours(1);
//                                dto.Messages.Add(mapper.Map<MessageDTO>(mess));
//                            }
//                        }
//                    }

//                    i++;
//                }
//                server.Channels.Add(dto);
//            }

//            //GET USERS
//            mapper = mapConfig.UserToServerUserListDTO.CreateMapper();
//            foreach (ServerUser su in await context.ServersUsers.Where(x => x.Id.Contains(serverId)).ToListAsync())
//            {
//                string id = su.Id.Split(':')[0];
//                server.Users
//                    .Add(mapper.Map<ServerUserListDTO>(
//                        await context.Users.FirstOrDefaultAsync(x => x.Id == id)
//                        ));
//            }

//            return server;
//        }

//        //FLYTTET TIL CHANNELSERVICE
//        //public async Task<ChannelDTO> LoadChannel(string channelId)
//        //{

//        //    //GET CHANNEL
//        //    var mapper = mapConfig.ChannelToChannelDTO.CreateMapper();
//        //    ChannelDTO chan = mapper.Map<ChannelDTO>(await context.Channels.FirstOrDefaultAsync(x => x.Id == channelId));

//        //    //ADD MESSAGES TO CHANNELDTO
//        //    MongoClient mClient = new MongoClient();
//        //    IMongoDatabase db = mClient.GetDatabase("discord");
//        //    var collection = db.GetCollection<MongoMessages>("DiscordMessages");
//        //    var filter = Builders<MongoMessages>.Filter.Eq("_id", channelId);
//        //    MongoMessages mMessages = await collection.Find(filter).FirstOrDefaultAsync();
//        //    if (mMessages == null)
//        //        return chan;
//        //    mapper = mapConfig.MessageToMessageDTO.CreateMapper();
//        //    if (mMessages.Messages.Count > 0)
//        //    {
//        //        foreach (Message mess in mMessages.Messages.OrderBy(x => x.CreatedAt).Take(100).ToList())
//        //        {
//        //            mess.CreatedAt = mess.CreatedAt.AddHours(1);
//        //            chan.Messages.Add(mapper.Map<MessageDTO>(mess));
//        //        }
//        //    }
//        //    return chan;
//        //}
//    }
//}
