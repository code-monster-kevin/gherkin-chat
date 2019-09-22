using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gherkin_chat.Entities
{
    /// <summary>
    /// Message
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// TimeStamp
        /// </summary>
        public DateTime TimeStamp { get; set; }
        /// <summary>
        /// ChannelId
        /// </summary>
        public Guid ChannelId { get; set; }
        /// <summary>
        /// Channel
        /// </summary>
        public Channel Channel { get; set; }
        /// <summary>
        /// UserId
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// User
        /// </summary>
        public User User { get; set; }
    }
}
