using System;
using System.Linq;
using System.Web.Http;
using Messager.Api.Properties;
using Messager.DataLayer;
using Messager.DataLayer.Sql;
using Messager.Model;

namespace Messager.Api.Controllers
{
    public class MessagesController : ApiController
    {
        private readonly NLog.ILogger _logger = Logger.Logger.Instance;
        private readonly IChatsRepository _chatsRepository;
        private readonly IMessagesRepository _messagesRepository;
        private readonly string _connectionString = Settings.Default.ConnectionString;

        public MessagesController()
        {
            IUsersRepository usersRepository = new UsersRepository(_connectionString);
            _chatsRepository = new ChatsRepository(_connectionString, usersRepository);
            _messagesRepository = new MessagesRepository(_connectionString, usersRepository, _chatsRepository);
        }

        [HttpPost]
        [Route("api/messages")]
        public Message Create([FromBody] Message message)
        {
            var createdMessage = _messagesRepository.CreateMessage(message);
            _logger.Info($"{DateTime.Now.ToShortDateString()} Message with id: {createdMessage.Id} has been created");
            return createdMessage;
        }

        [HttpGet]
        [Route("api/{userId}/messages/{phrase}")]
        public Message[] SearchMessagesForUser(Guid userId, string phrase)
        {
            _logger.Info($"{DateTime.Now.ToShortDateString()} Messages with phrase: {phrase} has been quried");
            return _chatsRepository.SearchMessagesByPhraseForUser(userId, phrase).ToArray();
        }
    }
}
