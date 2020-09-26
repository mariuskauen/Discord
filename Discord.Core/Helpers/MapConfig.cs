using AutoMapper;
using Discord.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.Core.Helpers
{
    public class MapConfig
    {
        #region Entity->DTO       
        public MapperConfiguration UserToUserDTO = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>());
        public MapperConfiguration UserToSmallUserDTO = new MapperConfiguration(cfg => cfg.CreateMap<User, SmallUserDTO>());
        public MapperConfiguration UserToServerUserListDTO = new MapperConfiguration(cfg => cfg.CreateMap<User, ServerUserListDTO>());

        public MapperConfiguration FriendListToDto = new MapperConfiguration(cfg => cfg.CreateMap<User, FriendListDTO>());

        public MapperConfiguration RequestToRequestDTO = new MapperConfiguration(cfg => cfg.CreateMap<FriendRequest, FriendRequestDTO>());

        public MapperConfiguration MessageToMessageDTO = new MapperConfiguration(cfg => cfg.CreateMap<Message, MessageDTO>());
        public MapperConfiguration MessageDTOToMessage = new MapperConfiguration(cfg => cfg.CreateMap<MessageDTO, Message>());

        public MapperConfiguration ServerToServerDTO = new MapperConfiguration(cfg => cfg.CreateMap<Server, ServerDTO>());
        public MapperConfiguration ServerToServerListDTO = new MapperConfiguration(cfg => cfg.CreateMap<Server, ServerListDTO>());

        public MapperConfiguration ChannelToChannelListDTO = new MapperConfiguration(cfg => cfg.CreateMap<Channel, ChannelListDTO>());
        public MapperConfiguration ChannelToChannelDTO = new MapperConfiguration(cfg => cfg.CreateMap<Channel, ChannelDTO>());


        #endregion


        #region Resolvers
        //public class MessageResolver : IValueResolver<Channel, ChannelDTO, List<MessageDTO>>
        //{
        //    public int Resolve(Channel source, ChannelDTO destination, List<MessageDTO> destList, ResolutionContext context)
        //    {
        //        return destList;
        //    }
        //}

        #endregion

    }
}
