using System;
using System.Linq;
using Gherkin_chat.Entities;

namespace Gherkin_chat.Repositories
{
    /// <summary>
    /// interface IMessageRepositories
    /// </summary>
    public interface IMessageRepositories
    {
        /// <summary>
        /// AddUser(User user);
        /// </summary>
        /// <param name="user"></param>
        void AddUser(User user);
        /// <summary>
        /// AddChannel(Channel channel);
        /// </summary>
        /// <param name="channel"></param>
        void AddChannel(Channel channel);
        /// <summary>
        /// AddMessage(Message message);
        /// </summary>
        /// <param name="message"></param>
        void AddMessage(Message message);
        /// <summary>
        /// GetUser(string name);
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        User GetUser(string name);
        /// <summary>
        /// GetChannels();
        /// </summary>
        /// <returns></returns>
        IQueryable<Channel> GetChannels();
        /// <summary>
        /// GetMessage(Guid channelId);
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        IQueryable<Message> GetMessage(Guid channelId);
        /// <summary>
        /// Save()
        /// </summary>
        /// <returns></returns>
        bool Save();
    }
}