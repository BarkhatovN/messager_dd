using System;
using System.Linq;
using System.Web.Http;
using Messager.Api.Properties;
using Messager.DataLayer;
using Messager.DataLayer.Sql;
using Messager.Model;

namespace Messager.Api.Controllers
{
    public class ChatsController : ApiController
    {
        private readonly IChatsRepository _chatsRepository;
        private readonly IMessagesRepository _messagesRepository;
        private readonly String _connectionString = Settings.Default.ConnectionString;

        public ChatsController()
        {
            IUsersRepository usersRepository = new UsersRepository(_connectionString);
            _chatsRepository = new ChatsRepository(_connectionString, usersRepository);
            _messagesRepository = new MessagesRepository(_connectionString, usersRepository, _chatsRepository);
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
        }

        [HttpPut]
        [Route("api/chats/{chatId}/{userId}")]
        public void DeleteMember(Guid chatId, Guid userId)
        {
            _chatsRepository.DeleteMember(chatId, userId);
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
        public Message[] GetMessagesForUser(Guid userId, Guid chatId)
        {
            return _messagesRepository.GetMessagesForUser(chatId, userId).ToArray();
        }
    }
}
