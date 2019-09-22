using AutoMapper;
using Gherkin_chat.DTO;
using Gherkin_chat.Entities;
using Gherkin_chat.SignalRHub;
using Gherkin_chat.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gherkin_chat.Controllers
{
    /// <summary>
    /// API for Messages functions
    /// </summary>
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        private readonly IMessageRepositories _messageRepositories;
        private readonly IMapper _mapper;
        private readonly IHubContext<MessageHub> _messageHub;

        /// <summary>
        /// Constructor for Messages Controller
        /// </summary>
        /// <param name="messageHub"></param>
        /// <param name="messageRepositories"></param>
        /// <param name="mapper"></param>
        public MessagesController(IHubContext<MessageHub> messageHub, IMessageRepositories messageRepositories, IMapper mapper)
        {
            _messageHub = messageHub;
            _messageRepositories = messageRepositories;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns the last 25 messages for a particular channel
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllMessage(string channel)
        {
            Channel existingChannel = _messageRepositories.GetChannels().FirstOrDefault(x => x.Name == channel);
            if (existingChannel != null)
            {
                List<Message> messages = _messageRepositories.GetMessage(existingChannel.Id)
                    .OrderByDescending(x => x.TimeStamp)
                    .Take(25)
                    .ToList();

                var allMessagesDto = messages.OrderBy(x => x.TimeStamp).Select(message => MapMessageToDto(message));

                return Ok(allMessagesDto);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Sends a new message to the channel
        /// </summary>
        /// <param name="messageDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddMessage([FromBody] MessageCreateDto messageDto)
        {
            Channel selectedChannel = _messageRepositories.GetChannels().FirstOrDefault(x => x.Name == messageDto.Channel);
            if (selectedChannel == null)
            {
                selectedChannel = new Channel
                {
                    Id = Guid.NewGuid(),
                    Name = messageDto.Channel
                };
                _messageRepositories.AddChannel(selectedChannel);
            }

            User chatUser = _messageRepositories.GetUser(messageDto.User);
            if (chatUser == null)
            {
                chatUser = new User
                {
                    Id = Guid.NewGuid(),
                    Name = messageDto.User
                };
                _messageRepositories.AddUser(chatUser);
            }

            Message newMessage = new Message
            {
                Id = Guid.NewGuid(),
                Value = messageDto.Value,
                User = chatUser,
                UserId = chatUser.Id,
                Channel = selectedChannel,
                ChannelId = selectedChannel.Id,
                TimeStamp = DateTime.UtcNow
            };

            _messageRepositories.AddMessage(newMessage);

            bool result = _messageRepositories.Save();
            if (!result)
            {
                return new StatusCodeResult(500);
            }

            var chatMessage = MapMessageToDto(newMessage);

            await _messageHub.Clients.All.SendAsync("NewMessage", chatMessage);

            return Ok(newMessage);
        }

        private MessageDto MapMessageToDto(Message message)
        {
            var chatMessage = new MessageDto
            {
                Id = message.Id,
                Value = message.Value,
                TimeStamp = message.TimeStamp.ToString(),
                Channel = message.Channel.Name,
                User = message.User.Name
            };

            return chatMessage;
        }
    }
}
