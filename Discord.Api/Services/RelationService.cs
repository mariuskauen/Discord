//using Discord.Api.Data;
//using Discord.Core.Helpers;
//using Discord.Core.Models;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Discord.Api.Services
//{
//    public class RelationService
//    {
//        private readonly DataContext context;
//        private readonly MapConfig mapConfig;
//        public RelationService(DataContext context, MapConfig mapConfig)
//        {
//            this.context = context;
//            this.mapConfig = mapConfig;
//        }
//        public async Task<List<FriendRequest>> GetFriendRequests(string userId)
//        {
//            return await context.Requests.Where(x => x.Id.Contains(userId) && x.IsActive == true).ToListAsync();
//        }

//        public async Task<List<FriendListDTO>> GetMyFriends(string userId)
//        {
//            var mapper = mapConfig.FriendListToDto.CreateMapper();
//            List<FriendListDTO> friendsToSend = new List<FriendListDTO>();
//            foreach(FriendShip f in await context.Friends.Where(x => x.Id.Contains(userId) && x.IsActive == true).ToListAsync())
//            {
//                string friendId = "";
//                string[] id = f.Id.Split(':');
//                foreach(string str in id)
//                {
//                    if (str != userId)
//                        friendId = str;
//                }
//                friendsToSend.Add(mapper.Map<FriendListDTO>(await context.Users.FirstOrDefaultAsync(x => x.Id == friendId)));
//            }
//            return friendsToSend;
//        }

//        public async Task<string> SendRequest(User me, string receiverId)
//        {
//            List<FriendShip> friends = await context.Friends.Where(x => x.Id.Contains(me.Id) && x.IsActive == true).ToListAsync();
//            List<FriendRequest> myRequests = await GetFriendRequests(me.Id);
//            FriendRequest request = null;

//            //Check if you are friends already
//            foreach (FriendShip f in friends)
//            {
//                if (f.IsActive)
//                {
//                    if (f.Id.Contains(me.Id) && f.Id.Contains(receiverId))
//                    {
//                        return "You are already friends!";
//                    }
//                }
//            }
//            //Check if there is an existing friend request
//            foreach (FriendRequest r in myRequests)
//            {

//                if (r.IsActive)
//                {
//                    if (r.Id.StartsWith(me.Id) && r.Id.Contains(receiverId))
//                    {
//                        return "There is already a request between you! Id: " + r.Id;
//                    }
//                }
//                if (!r.IsActive)
//                {
//                    if (r.Id.StartsWith(me.Id) && r.Id.Contains(receiverId))
//                    {
//                        request = r;
//                        break;
//                    }
//                }
//            }
//            if (request == null)
//            {
//                request = new FriendRequest()
//                {
//                    Id = me.Id + ":" + receiverId,
//                    SenderId = me.Id,
//                    ReceiverId = receiverId,
//                    IsActive = true
//                };
//                context.Requests.Add(request);
//            }
//            else
//            {
//                request.IsActive = true;
//            }           
//            await context.SaveChangesAsync();
//            return "Ok";
//        }

//        public async Task<string> AcceptRequest(string userId, string requestId)
//        {
//            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == userId);
//            if (user == null)
//                return "Who are you? O_o o_O";

//            FriendRequest req = await context.Requests.FirstOrDefaultAsync(x => x.Id == requestId);
//            if (req == null)
//                return "Could not find request";

//            if (!req.IsActive)
//                return "Can't accept, inactive friend request";
//            if (req.SenderId == userId)
//                return "You can't accept your own request!";
//            if (req.ReceiverId != userId)
//                return "Who's request are you trying to accept here?";

//            FriendShip fs = new FriendShip()
//            {
//                Id = req.Id,
//                IsActive = true
//            };
//            req.IsActive = false;
//            context.Friends.Add(fs);
//            await context.SaveChangesAsync();

//            return "Ok";
//        }
//    }
//}
