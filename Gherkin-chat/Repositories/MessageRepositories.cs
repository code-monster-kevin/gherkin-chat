using Gherkin_chat.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Gherkin_chat.Repositories
{
    /// <summary>
    /// MessageRepositories
    /// </summary>
    public class MessageRepositories : IMessageRepositories
    {
        private GherkinDbContext _context;

        /// <summary>
        /// MessageRepositories
        /// </summary>
        /// <param name="context"></param>
        public MessageRepositories(GherkinDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GetMessage(Guid channelId)
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public IQueryable<Message> GetMessage(Guid channelId)
        {
            return _context.Messages
                .Include(x => x.Channel)
                .Include(x => x.User)
                .Where(x => x.ChannelId == channelId).OrderByDescending(x => x.TimeStamp);
        }

        /// <summary>
        /// GetChannels()
        /// </summary>
        /// <returns></returns>
        public IQueryable<Channel> GetChannels()
        {
            return _context.Channels.OrderBy(x => x.Name);
        }

        /// <summary>
        /// GetUser(string name)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public User GetUser(string name)
        {
            return _context.Users.FirstOrDefault(x => x.Name == name);
        }

        /// <summary>
        /// AddChannel(Channel channel)
        /// </summary>
        /// <param name="channel"></param>
        public void AddChannel(Channel channel)
        {
            _context.Channels.Add(channel);
        }

        /// <summary>
        /// AddUser(User user)
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        /// <summary>
        /// AddMessage(Message message)
        /// </summary>
        /// <param name="message"></param>
        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
        }

        /// <summary>
        /// Save()
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
