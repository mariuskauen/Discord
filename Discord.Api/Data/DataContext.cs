using Discord.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discord.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        //public DbSet<User> Users { get; set; }
        //public DbSet<FriendShip> Friends { get; set; }

        //public DbSet<FriendRequest> Requests { get; set; }

        //public DbSet<Server> Servers { get; set; }

        //public DbSet<Channel> Channels { get; set; }

        //public DbSet<Message> Messages { get; set; }

        //public DbSet<ServerUser> ServersUsers { get; set; }

        //public DbSet<Conversation> Conversations { get; set; }
    }
}
