using Discord.Api.Data;
using Discord.Core.Helpers;
using Discord.Core.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discord.Api.Services
{
    public class ChannelService
    {
        private readonly DataContext context;
        private readonly MapConfig mapConfig;

        public ChannelService(DataContext context, MapConfig mapConfig)
        {
            this.context = context;
            this.mapConfig = mapConfig;
        }

        public async Task<ChannelDTO> GetChannel(string channelId)
        {

            //GET CHANNEL
            var mapper = mapConfig.ChannelToChannelDTO.CreateMapper();
            ChannelDTO chan = mapper.Map<ChannelDTO>(await context.Channels.FirstOrDefaultAsync(x => x.Id == channelId));

            //ADD MESSAGES TO CHANNELDTO
            MongoClient mClient = new MongoClient();
            IMongoDatabase db = mClient.GetDatabase("discord");
            var collection = db.GetCollection<MongoMessages>("DiscordMessages");
            var filter = Builders<MongoMessages>.Filter.Eq("_id", channelId);
            MongoMessages mMessages = await collection.Find(filter).FirstOrDefaultAsync();
            if (mMessages == null)
                return chan;
            mapper = mapConfig.MessageToMessageDTO.CreateMapper();
            if (mMessages.Messages.Count > 0)
            {
                foreach (Message mess in mMessages.Messages.OrderBy(x => x.CreatedAt).Take(50).ToList())
                {
                    mess.CreatedAt = mess.CreatedAt.AddHours(1);
                    chan.Messages.Add(mapper.Map<MessageDTO>(mess));
                }
            }
            return chan;
        }
    }
}
