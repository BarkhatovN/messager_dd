using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Linq;
using Messager.DataLayer;
using Messager.DataLayer.Sql;
using Messager.Model;

namespace Messager.DataLayer.Sql.Tests
{
    [TestClass]
    public class MessagesRepositoryTests
    {
        private readonly String ConnectionString = @"Server=POTM-PC\SQLEXPRESS;Database=messager_db;User Id=potm2;Password=12312322;";

        private List<Guid> _tmpMessages = new List<Guid>();
        private List<Guid> _tmpChats = new List<Guid>();
        private List<Guid> _tmpUsers = new List<Guid>();

        private List<User> Users;
        private Message Message;
        private Chat Chat;

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
            var createdUser1 = usersRepository.CreateUser(user1);
            var createdUser2 = usersRepository.CreateUser(user2);
            var createdUser3 = usersRepository.CreateUser(user3);

            Users = new List<User>{ createdUser1, createdUser2, createdUser3 };
            _tmpUsers.AddRange(new[] { createdUser1.Id, createdUser2.Id, createdUser3.Id });

            Chat = new Chat
            {
                Name = "ChatName",
                Creater = createdUser1,
                Members = Users
            };

            var chatRepository = new ChatsRepository(ConnectionString, usersRepository);
            Chat = chatRepository.CreateChat(Chat);
            _tmpChats.Add(Chat.Id);

            var attachments = new List<byte[]>
            {
                Encoding.UTF8.GetBytes("Image"),
                Encoding.UTF8.GetBytes("Image2"),
            };

            Message = new Message
            {
                User = createdUser1,
                Attachments = attachments,
                Chat = Chat,
                Date = DateTime.Now.ToUniversalTime(),
                IsSelfDestructing = false,
                Text = "Hello Tester, How are you?"
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
            var message = messagesRepository.CreateMessage(Message);
            _tmpMessages.Add(message.Id);

            //assert
            Assert.AreEqual(Message.Text, message.Text);
            Assert.AreEqual(Message.Date, message.Date);
            Assert.AreEqual(Message.Chat.Id, message.Chat.Id);
            Assert.AreEqual(Message.User.Id, message.User.Id);
        }

        [TestMethod]
        public void ShouldDeleteMessage()
        {
            //arrange
            var usersRepository = new UsersRepository(ConnectionString);
            var chatsRepository = new ChatsRepository(ConnectionString, usersRepository);
            var messagesRepository = new MessagesRepository(ConnectionString, usersRepository, chatsRepository);
            var message = messagesRepository.CreateMessage(Message);
            
            //act
            messagesRepository.DeleteMessage(message.Id);

            //assert
            Assert.ThrowsException<ArgumentException>(()=>messagesRepository.GetMessage(message.Id));
        }

        [TestCleanup]
        public void Clean()
        {
            var usersRepository = new UsersRepository(ConnectionString);
            foreach (var userId in _tmpUsers)
                usersRepository.DeleteUser(userId);

            var chatsRepository = new ChatsRepository(ConnectionString, usersRepository);
            foreach (var chatId in _tmpChats)
                chatsRepository.DeleteChat(chatId);

            var messagesRepository = new MessagesRepository(ConnectionString, usersRepository,chatsRepository);
            foreach (var messageId in _tmpMessages)
                messagesRepository.DeleteMessage(messageId);

        }
    }
}
