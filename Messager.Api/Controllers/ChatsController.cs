using Messager.DataLayer;
using Messager.DataLayer.Sql;
using Messager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Services.Description;

namespace Messager.Api.Controllers
{
    public class ChatsController : ApiController
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IChatsRepository _chatsRepository;
        private readonly IMessagesRepository _messagesRepository;
        private readonly String ConnectionString = Properties.Settings.Default.ConnectionString;

        public ChatsController()
        {
            _usersRepository = new UsersRepository(ConnectionString);
            _chatsRepository = new ChatsRepository(ConnectionString, _usersRepository);
            _messagesRepository = new MessagesRepository(ConnectionString, _usersRepository, _chatsRepository);
        }

        [HttpPut]
        [Route("api/chats/{chatId}/{userId}")]
        public void AddMember(Guid chatId, Guid userId)
        {
            _chatsRepository.AddMember(chatId, userId);
        }

        [HttpPost]
        [Route("api/chats")]
        public Chat Create([FromBody] Chat chat)
        {
            return _chatsRepository.CreateChat(chat);
        }

        [HttpDelete]
        [Route("api/chats/{id}")]
        public void Delete(Guid id)
        {
             _chatsRepository.DeleteChat(id);
            return;
        }

        [HttpPut]
        [Route("api/chats/{chatId}/{userId}")]
        public void DeleteMember(Guid chatId, Guid userId)
        {
            _chatsRepository.DeleteMember(chatId, userId);
            return;
        }

        [HttpGet]
        [Route("api/chats/{id}")]
        public Chat GetInfo(Guid id)
        {
            return _chatsRepository.GetChatInfo(id);
        }

        [HttpGet]
        [Route("api/{userId}/chats/{chatId}")]
        public Chat Get(Guid userId, Guid chatId)
        {
            return _chatsRepository.GetChat(chatId, userId);
        }

        [HttpGet]
        [Route("api/{userId}/chats/{chatId}/messages")]
        public Model.Message[] GetMessagesForUser(Guid userId, Guid chatId)
        {
            return _messagesRepository.GetMessagesForUser(chatId, userId).ToArray();
        }
    }
}
