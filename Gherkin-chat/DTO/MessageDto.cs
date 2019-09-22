using System;

namespace Gherkin_chat.DTO
{
    /// <summary>
    /// MessageDto
    /// </summary>
    public class MessageDto
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
        public string TimeStamp { get; set; }
        /// <summary>
        /// Channel
        /// </summary>
        public string Channel { get; set; }
        /// <summary>
        /// User
        /// </summary>
        public string User { get; set; }
    }
}
