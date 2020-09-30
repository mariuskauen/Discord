using Discord.Api.Data;
using Discord.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Discord.Core.Data;

namespace Discord.Api.Services
{
    public class UserService
    {
        private readonly DataContext _context;
        private readonly ConversationService _conSer;
        private readonly QueryRepository _query;
        public UserService(DataContext context, ConversationService conSer, QueryRepository query)
        {
            _context = context;
            _conSer = conSer;
            _query = query;
        }
        public async Task InitializeUser(User user)
        {
            user.Status = Status.Online;
            user.Lastname = "Doe";
            user.Firstname = "John/Jane";
            user.DateJoined = DateTime.UtcNow;
            user.LastOnline = DateTime.UtcNow;
            user.ProfilePicture = "something";
        }

        public async Task<UserDTO> GetUser(string query)
        {
            //string query = "Users:" + "_id:" + id;

            List<UserDTO> users = await _query.GetList(new User(), new UserDTO(), query);
            return users.FirstOrDefault();
        }
    }
}
