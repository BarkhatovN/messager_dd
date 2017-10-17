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
        private List<ChatsRepositoryTests.ChatIdMemberId> _tempChatIdMemberIds = new List<ChatsRepositoryTests.ChatIdMemberId>();

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

            Users = new List<User>
            {
                usersRepository.CreateUser(user1),
                usersRepository.CreateUser(user2),
                usersRepository.CreateUser(user3)
            };
            _tmpUsers.AddRange(Users.Select(u => u.Id));

            Chat = new Chat
            {
                Name = "ChatName",
                Creater = Users[0],
                Members = Users
            };

            var chatRepository = new ChatsRepository(ConnectionString, usersRepository);
            Chat = chatRepository.CreateChat(Chat);
            _tmpChats.Add(Chat.Id);
            foreach (var m in Chat.Members)
                _tempChatIdMemberIds.Add(new ChatsRepositoryTests.ChatIdMemberId
                {
                    ChatId = Chat.Id,
                    MemberId = m.Id
                });

            var attachments = new List<byte[]>
            {
                Encoding.UTF8.GetBytes("Image"),
                Encoding.UTF8.GetBytes("Image2"),
            };

            Message = new Message
            {
                User = Users[0],
                Chat = Chat,
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
            var message = messagesRepository.CreateMessage(Message);
            _tmpMessages.Add(message.Id);

            //assert
            Assert.AreEqual(Message.Text, message.Text);
            Assert.AreEqual(Message.Date, message.Date);
            Assert.AreEqual(Message.Chat.Id, message.Chat.Id);
            Assert.AreEqual(Message.User.Id, message.User.Id);
        }

        [TestMethod]
        public void ShouldGetMessage()
        {
            //arrange
            var usersRepository = new UsersRepository(ConnectionString);
            var chatsRepository = new ChatsRepository(ConnectionString, usersRepository);
            var messagesRepository = new MessagesRepository(ConnectionString, usersRepository, chatsRepository);
            var createdMessage = messagesRepository.CreateMessage(Message);
            _tmpMessages.Add(createdMessage.Id);

            //act
            var gottenMessage = messagesRepository.GetMessage(createdMessage.Id);

            //assert
            Assert.AreEqual(createdMessage.Text, gottenMessage.Text);
            Assert.AreEqual(createdMessage.IsSelfDestructing, gottenMessage.IsSelfDestructing);
            Assert.AreEqual(createdMessage.Date.ToString(), gottenMessage.Date.ToString());
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
