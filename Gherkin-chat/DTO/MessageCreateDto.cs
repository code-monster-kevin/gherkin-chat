using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gherkin_chat.DTO
{
    /// <summary>
    /// MessageCreateDto
    /// </summary>
    public class MessageCreateDto
    {
        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }
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
