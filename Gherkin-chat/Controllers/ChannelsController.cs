using AutoMapper;
using Gherkin_chat.DTO;
using Gherkin_chat.Entities;
using Gherkin_chat.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Gherkin_chat.Controllers
{
    /// <summary>
    /// API for Channels functions
    /// </summary>
    [Route("api/[controller]")]
    public class ChannelsController : Controller
    {
        private IMessageRepositories _messageRepositories;
        private IMapper _mapper;

        /// <summary>
        /// Constructor for Channels Controller
        /// </summary>
        /// <param name="messageRepositories"></param>
        /// <param name="mapper"></param>
        public ChannelsController(IMessageRepositories messageRepositories, IMapper mapper)
        {
            _messageRepositories = messageRepositories;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all the available channels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllChannels()
        {
            List<Channel> allChannels = _messageRepositories.GetChannels().ToList();

            var allChannelsDto = allChannels.Select(channel => _mapper.Map<ChannelDto>(channel));

            return Ok(allChannelsDto);
        }

        /// <summary>
        /// Create a new channel
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddChannel(string channel)
        {
            Channel existingChannel = _messageRepositories.GetChannels().FirstOrDefault(x => x.Name == channel);

            if (existingChannel != null)
            {
                return Ok(existingChannel);
            }

            Channel newChannel = new Channel
            {
                Id = Guid.NewGuid(),
                Name = channel
            };

            _messageRepositories.AddChannel(newChannel);

            bool result = _messageRepositories.Save();
            if (!result)
            {
                return new StatusCodeResult(500);
            }

            return Ok(newChannel);
        }
    }
}
