using Discord.Api.Data;
using Discord.Core.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discord.Api.Services
{
    public class AuthService
    {
        private readonly DataContext _context;
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<User> collection;
        public AuthService(DataContext context, IMongoSettings settings)
        {
            _context = context;
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);
            collection = database.GetCollection<User>("Users");
        }

        public async Task<User> Login(string username, string password)
        {
            var filter = Builders<User>.Filter.Eq("Username", username);
            var user = await collection.Find(filter).FirstOrDefaultAsync();
            if (user == null)
                return null;
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await collection.InsertOneAsync(user);

            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            var filter = Builders<User>.Filter.Eq("username", username);
            if (await collection.Find(filter).FirstOrDefaultAsync() == null)
                return false;
            return true;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> IdExists(string Id)
        {
            var filter = Builders<User>.Filter.Eq("_id", Id);
            if (await collection.Find(filter).FirstOrDefaultAsync() == null)
                return false;
            return true;
        }
    }
}
