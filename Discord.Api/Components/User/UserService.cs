using Discord.Api.Data;
using Discord.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Discord.Api.Data.Repositories;

namespace Discord.Api.Components.User
{
    public class UserService
    {
        private readonly QueryRepository _query;
        private readonly CommandRepository _command;
        public UserService(QueryRepository query, CommandRepository command)
        {
            _query = query;
            _command = command;
        }
        public async Task InitializeUser(string Id, string Username)
        {
            string query = "Users";
            //Create user settings
            User newUser = new User()
            {
                Id = Id,
                Username = Username,
                Firstname = "",
                Lastname = "",
                LastOnline = DateTime.Now,
                DateJoined = DateTime.Now,
                Status = Status.Online,
                Settings = "",
                ProfilePicture = "",
                Conversations = new List<string>(),
                Servers = new List<string>(),
                FriendShips = new List<string>(),
                FriendRequests = new List<string>()
            };

            await _command.Post(newUser, query);

            return;
        }

        public async Task<UserDTO> GetUser(string query)
        {
            //string query = "Users:" + "_id:" + id;

            List<UserDTO> users = await _query.GetList(new User(), new UserDTO(), query);
            return users.FirstOrDefault();
        }
    }
}
