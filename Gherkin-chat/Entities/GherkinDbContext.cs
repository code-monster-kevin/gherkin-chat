using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Gherkin_chat.Entities
{
    /// <summary>
    /// GherkinDbContext
    /// </summary>
    public class GherkinDbContext : DbContext
    {
        /// <summary>
        /// GherkinDbContext
        /// </summary>
        /// <param name="options"></param>
        public GherkinDbContext(DbContextOptions<GherkinDbContext> options): base(options)
        {

        }

        /// <summary>
        /// Channels
        /// </summary>
        public DbSet<Channel> Channels { get; set; }
        /// <summary>
        /// Users
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Messages
        /// </summary>
        public DbSet<Message> Messages { get; set; }

        /// <summary>
        /// OnModelCreating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Channel>().HasData(
                new Channel
                {
                    Id = new Guid("81d26ee8-b2df-43b0-ac7f-ce692d3d0b77"),
                    Name = "Lobby"
                });
        }
    }
}
