using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Gherkin_chat.Entities;

namespace Gherkin_chat.DTO
{
    /// <summary>
    /// Mapping Profile for Automapper
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Mapping Profile Constructor
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Message, MessageCreateDto>().ReverseMap();
            CreateMap<Message, MessageDto>().ReverseMap();
            CreateMap<Channel, ChannelDto>().ReverseMap();
        }        
    }
}
