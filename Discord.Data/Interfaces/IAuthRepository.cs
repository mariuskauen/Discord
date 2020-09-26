using Discord.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord.Data.Interfaces
{
    public interface IAuthRepository
    {
        Task<bool> IsUsernameAvailable(string username);
        Task<User> CreateUser(User user);
    }
}
