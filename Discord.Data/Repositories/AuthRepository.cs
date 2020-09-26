using Discord.Core.Models;
using Discord.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord.Data.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext context;

        public AuthRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<User> CreateUser(User user)
        {
            
        }

        public async Task<bool> IsUsernameAvailable(string username)
        {
            return await context.Users
                .AsNoTracking()
                .AnyAsync(a => a.Username == username);
        }
    }
}
