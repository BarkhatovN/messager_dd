using System;
using System.Linq;
using System.Web.Http;
using Messager.Api.Properties;
using Messager.DataLayer;
using Messager.DataLayer.Sql;
using Messager.Model;
using Messager.Filters;

namespace Messager.Api.Controllers
{
    public class ChatsController : ApiController
    {
        private readonly NLog.ILogger _logger = Logger.Logger.Instance;
        private readonly IChatsRepository _chatsRepository;
        private readonly IMessagesRepository _messagesRepository;
        private readonly string _connectionString = Settings.Default.ConnectionString;

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
            _logger.Info($"{DateTime.Now.ToShortDateString()} User with Id: {userId} has been added to chat with Id: {chatId}");
            _chatsRepository.AddMember(chatId, userId);
        }

        [HttpPost]
        [Route("api/chats")]
        public Chat Create([FromBody] Chat chat)
        {
            try
            {
                var createdChat = _chatsRepository.CreateChat(chat);
                _logger.Info($"Chat with Id: {chat.Id} has been added");
                return createdChat;
            }
            catch (ArgumentException ex)
            {
                _logger.Error($"{DateTime.Now.ToShortDateString()} Chat with Id: {chat.Id} has NOT been added because of {ex.Message}");
            }
            return null;
        }

        [HttpDelete]
        [Route("api/chats/{id}")]
        public void Delete(Guid id)
        {
            _logger.Info($"{DateTime.Now.ToShortDateString()} Chat with {id} has been deleted");
            _chatsRepository.DeleteChat(id);
        }

        [HttpPut]
        [Route("api/chats/{chatId}/{userId}")]
        public void DeleteMember(Guid chatId, Guid userId)
        {
            _chatsRepository.DeleteMember(chatId, userId);
            _logger.Info($"{DateTime.Now.ToShortDateString()} From chat with id:{chatId} member with id: {userId} has been deleted");
        }

        [HttpGet]
        [Route("api/chats/{userId}")]
        public Chat[] GetUserChatsInfo(Guid userId)
        {
            var chats = _chatsRepository.GetUserChatsInfo(userId);
            _logger.Info($"{DateTime.Now.ToShortDateString()} Chat info with id: {userId} has been queried");
            return chats;
        }

        [HttpGet]
        [Route("api/{userId}/chats/{chatId}")]
        public Chat Get(Guid userId, Guid chatId)
        {
            _logger.Info($"{DateTime.Now.ToShortDateString()} Chat with id: {chatId} has been queried");
            return _chatsRepository.GetChat(chatId, userId);
        }

        [HttpGet]
        [Route("api/users/{userId}/chats/{chatId}/messages")]
        public Message[] GetMessagesForUser(Guid userId, Guid chatId)
        {
            _logger.Info($"{DateTime.Now.ToShortDateString()} Messages from chat with id: {chatId} for user with id: {userId} has been queried");
            var messages = _messagesRepository.GetMessagesForUser(chatId, userId);
            return (messages != null)? messages : new Message[0] ;
        }
    }
}
