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
        private readonly IUsersRepository _usersRepository;
        private readonly IChatsRepository _chatsRepository;
        private readonly IMessagesRepository _messagesRepository;
        private readonly String ConnectionString = Settings.Default.ConnectionString;

        public MessagesController()
        {
            _usersRepository = new UsersRepository(ConnectionString);
            _chatsRepository = new ChatsRepository(ConnectionString, _usersRepository);
            _messagesRepository = new MessagesRepository(ConnectionString, _usersRepository, _chatsRepository);
        }

        [HttpPost]
        [Route("api/messages")]
        public Message Create([FromBody] Message message)
        {
            return _messagesRepository.CreateMessage(message);
        }

        [HttpGet]
        [Route("api/{userId}/messages/{phrase}")]
        public Message[] SearchMessagesForUser(Guid userId,String phrase)
        {
            return _chatsRepository.SearchMessagesByPhraseForUser(userId, phrase).ToArray();
        }
    }
}
