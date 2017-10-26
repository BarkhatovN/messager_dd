using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Linq;
using Messager.Model;


namespace Messager.DataLayer.Sql.Tests
{
    [TestClass]
    public class MessagesRepositoryTests
    {
        private const string ConnectionString = @"Server=POTM-PC\SQLEXPRESS;Database=messager_db;User Id=potm2;Password=12312322;";

        private readonly List<Guid> _tmpMessages = new List<Guid>();
        private readonly List<Guid> _tmpChats = new List<Guid>();
        private readonly List<Guid> _tmpUsers = new List<Guid>();
        private readonly List<ChatsRepositoryTests.ChatIdMemberId> _tempChatIdMemberIds = new List<ChatsRepositoryTests.ChatIdMemberId>();

        private List<User> _users;
        private Message _message;
        private Chat _chat;

        [TestInitialize]
        public void Initialize()
        {
            var user1 = new User
            {
                FirstName = "Thomas",
                LastName = "Anderson",
                Login = "Neo",
                Password = "123123123"
            };

            var user2 = new User
            {
                FirstName = "Smith",
                LastName = "Smith",
                Login = "Agent Smith",
                Password = "123123123",
            };

            var user3 = new User
            {
                FirstName = "Noname",
                LastName = "Noname",
                Login = "Trinity",
                Password = "123123123",
            };

            var usersRepository = new UsersRepository(ConnectionString);

            _users = new List<User>
            {
                usersRepository.CreateUser(user1),
                usersRepository.CreateUser(user2),
                usersRepository.CreateUser(user3)
            };
            _tmpUsers.AddRange(_users.Select(u => u.Id));

            _chat = new Chat
            {
                Name = "ChatName",
                Creater = _users[0],
                Members = _users
            };

            var chatRepository = new ChatsRepository(ConnectionString, usersRepository);
            _chat = chatRepository.CreateChat(_chat);
            _tmpChats.Add(_chat.Id);
            foreach (var m in _chat.Members)
                _tempChatIdMemberIds.Add(new ChatsRepositoryTests.ChatIdMemberId
                {
                    ChatId = _chat.Id,
                    MemberId = m.Id
                });

            var attachments = new List<byte[]>
            {
                Encoding.UTF8.GetBytes("Image"),
                Encoding.UTF8.GetBytes("Image2"),
            };

            _message = new Message
            {
                User = _users[0],
                Chat = _chat,
                Date = DateTime.Now.ToUniversalTime(),
                IsSelfDestructing = false,
                Text = "Hello Tester, How are you?",
                Attachments = attachments
            };
        }

        [TestMethod]
        public void ShouldCreateMessage()
        {
            //arrange
            var usersRepository = new UsersRepository(ConnectionString);
            var chatsRepository = new ChatsRepository(ConnectionString, usersRepository);
            var messagesRepository = new MessagesRepository(ConnectionString, usersRepository, chatsRepository);

            //act
            var message = messagesRepository.CreateMessage(_message);
            _tmpMessages.Add(message.Id);

            //assert
            Assert.AreEqual(_message.Text, message.Text);
            Assert.AreEqual(_message.Date, message.Date);
            Assert.AreEqual(_message.Chat.Id, message.Chat.Id);
            Assert.AreEqual(_message.User.Id, message.User.Id);
        }

        [TestMethod]
        public void ShouldGetMessage()
        {
            //arrange
            var usersRepository = new UsersRepository(ConnectionString);
            var chatsRepository = new ChatsRepository(ConnectionString, usersRepository);
            var messagesRepository = new MessagesRepository(ConnectionString, usersRepository, chatsRepository);
            var createdMessage = messagesRepository.CreateMessage(_message);
            _tmpMessages.Add(createdMessage.Id);

            //act
            var gottenMessage = messagesRepository.GetMessage(createdMessage.Id);

            //assert
            Assert.AreEqual(createdMessage.Text, gottenMessage.Text);
            Assert.AreEqual(createdMessage.IsSelfDestructing, gottenMessage.IsSelfDestructing);
            Assert.AreEqual(createdMessage.Date.ToString(CultureInfo.InvariantCulture), gottenMessage.Date.ToString(CultureInfo.InvariantCulture));
            if (createdMessage.Attachments.ElementAt(0).Length == 
                    gottenMessage.Attachments.ElementAt(0).Length)
            {
                Assert.IsTrue(createdMessage.Attachments.ElementAt(0).
                      SequenceEqual(gottenMessage.Attachments.ElementAt(0)));
                Assert.IsTrue(createdMessage.Attachments.ElementAt(1).
                      SequenceEqual(gottenMessage.Attachments.ElementAt(1)));
            }
            else
            {
                Assert.IsTrue(createdMessage.Attachments.ElementAt(0).
                        SequenceEqual(gottenMessage.Attachments.ElementAt(1)));
                Assert.IsTrue(createdMessage.Attachments.ElementAt(1).
                        SequenceEqual(gottenMessage.Attachments.ElementAt(0)));
            }
        }

        [TestMethod]
        public void ShouldDeleteMessage()
        {
            //arrange
            var usersRepository = new UsersRepository(ConnectionString);
            var chatsRepository = new ChatsRepository(ConnectionString, usersRepository);
            var messagesRepository = new MessagesRepository(ConnectionString, usersRepository, chatsRepository);
            var message = messagesRepository.CreateMessage(_message);
            
            //act
            messagesRepository.DeleteMessage(message.Id);

            //assert
            Assert.ThrowsException<ArgumentException>(()=>messagesRepository.GetMessage(message.Id));
        }

        [TestCleanup]
        public void Clean()
        {
            var usersRepository = new UsersRepository(ConnectionString);
            var chatsRepository = new ChatsRepository(ConnectionString, usersRepository);
            var messagesRepository = new MessagesRepository(ConnectionString, usersRepository,chatsRepository);

            foreach (var cm in _tempChatIdMemberIds)
                chatsRepository.DeleteMember(cm.ChatId, cm.MemberId);

            foreach (var messageId in _tmpMessages)
                messagesRepository.DeleteMessage(messageId);

            foreach (var chatId in _tmpChats)
                chatsRepository.DeleteChat(chatId);

            foreach (var userId in _tmpUsers)
                usersRepository.DeleteUser(userId);
        }
    }
}
