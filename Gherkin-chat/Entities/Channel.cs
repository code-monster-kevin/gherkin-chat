using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gherkin_chat.Entities
{
    /// <summary>
    /// Channel
    /// </summary>
    public class Channel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
    }
}
