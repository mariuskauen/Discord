using Discord.Api.Data;
using Discord.Core.Helpers;
using Discord.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discord.Api.Services
{
    public class ServerService
    {
        private readonly DataContext context;
        private readonly MapConfig mapConfig;

        public ServerService(DataContext context, MapConfig mapConfig)
        {
            this.context = context;
            this.mapConfig = mapConfig;
        }
        public async Task<string> CreateServer(string serverName, string userId)
        {
            Server server = new Server()
            {
                Id = Guid.NewGuid().ToString(),
                OwnerId = userId,
                Name = serverName
            };
            context.Servers.Add(server);
            ServerUser su = new ServerUser()
            {
                Id = userId + ":" + server.Id
            };
            context.ServersUsers.Add(su);
            await context.SaveChangesAsync();

            //ADD GENERAL CHANNEL.

            return "Ok";
        }
        public async Task<List<ServerListDTO>> GetMyServers(string userId)
        {
            if (!await context.Users.AsNoTracking().AnyAsync(x => x.Id == userId))
                return null;

            List<ServerUser> serverUsers = await context.ServersUsers.Where(x => x.Id.Contains(userId)).ToListAsync();
            List<ServerListDTO> Servers = new List<ServerListDTO>();
            var mapper = mapConfig.ServerToServerListDTO.CreateMapper();
            foreach (ServerUser su in serverUsers)
            {
                string serverId = su.Id.Split(':')[1];
                Servers.Add(mapper.Map<ServerListDTO>(await context.Servers.FirstOrDefaultAsync(x => x.Id == serverId)));
            }
            return Servers;
        }

        public async Task<string> JoinServer(string userId, string serverId)
        {
            if (await context.ServersUsers.AsNoTracking().AnyAsync(x => x.Id == userId+":"+serverId))
                return "You are already in this server!";

            ServerUser su = new ServerUser()
            {
                Id = userId + ":" + serverId
            };
            context.ServersUsers.Add(su);
            await context.SaveChangesAsync();

            return "Ok";
        }

        public async Task<List<SmallUserDTO>> GetServerUsers(string serverId)
        {
            List<ServerUser> serverUsers = await context.ServersUsers.Where(x => x.Id.Contains(serverId)).ToListAsync();
            List<SmallUserDTO> users = new List<SmallUserDTO>();
            var mapper = mapConfig.UserToSmallUserDTO.CreateMapper();
            foreach (ServerUser su in serverUsers)
            {
                string userId = su.Id.Split(':')[0];
                users.Add(mapper.Map<SmallUserDTO>(await context.Users.FirstOrDefaultAsync(x => x.Id == userId)));
            }
            return users;
        }

    }
}
