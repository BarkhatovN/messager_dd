using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Messager.DataLayer.Sql.Tests.Properties;
using Messager.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Messager.DataLayer.Sql.Tests
{
    [TestClass]
    public class ChatsRepositoryTests
    {
        public struct ChatIdMemberId { public Guid ChatId; public Guid MemberId; }

        private readonly string _connectionString = Settings.Default.ConnectionString;

        private readonly List<Guid> _tempChatIds = new List<Guid>();
        private readonly List<Guid> _tempUserIds = new List<Guid>();
        private readonly List<Guid> _tempMessageIds = new List<Guid>();
        private readonly List<ChatIdMemberId> _tempChatIdMemberIds = new List<ChatIdMemberId>();

        private List<User> _users;
        private Chat _chat;
        private List<Message> _messages;

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
                Password = "123123123"
            };

            var user3 = new User
            {
                FirstName = "Noname",
                LastName = "Noname",
                Login = "Trinity",
                Password = "123123123"
            };

            var usersRepository = new UsersRepository(_connectionString);
            _users = new List<User>
            {
                usersRepository.CreateUser(user1),
                usersRepository.CreateUser(user2),
                usersRepository.CreateUser(user3)
            };
            _tempUserIds.AddRange(_users.Select(u=>u.Id));
            
            var chat = new Chat
            {
                Creater = _users[0],
                Members = new[] { _users[0], _users[1] },
                Name = "ChatName"
            };

            var chatsRepository = new ChatsRepository(_connectionString, usersRepository);
            _chat = chatsRepository.CreateChat(chat);
            _tempChatIds.Add(_chat.Id);
            _tempChatIdMemberIds.AddRange(new[]
            {
                new ChatIdMemberId{ ChatId = _chat.Id, MemberId = _users[0].Id },
                new ChatIdMemberId{ ChatId = _chat.Id, MemberId = _users[1].Id }
            });

            var message1 = new Message
            {
                Chat = _chat,
                Date = DateTime.Now.ToUniversalTime(),
                IsSelfDestructing = false,
                Text = "Message1",
                User = _users[0],
                Attachments = new[]
                {
                    Encoding.UTF8.GetBytes("Hello"),
                    Encoding.UTF8.GetBytes("World")
                }
            };

            var message2 = new Message
            {
                Chat = _chat,
                Date = DateTime.Now.ToUniversalTime(),
                IsSelfDestructing = false,
                Text = "Message2",
                User = _users[0]
            };

            var messagesRepository = new MessagesRepository(_connectionString, usersRepository, chatsRepository);
            _messages = new List<Message> 
            {
                messagesRepository.CreateMessage(message1),
                messagesRepository.CreateMessage(message2)
            };
            _tempMessageIds.AddRange(_messages.Select(m => m.Id));
            _chat.Messages = _messages;
        }

        [TestMethod]
        public void ShouldAddMember()
        {
            //arrange
            var usersRepository = new UsersRepository(_connectionString);
            var chatRepository = new ChatsRepository(_connectionString, usersRepository);

            //act
            chatRepository.AddMember(_chat.Id, _users[2].Id);
            _tempChatIdMemberIds.Add(new ChatIdMemberId { ChatId = _chat.Id, MemberId = _users[2].Id });
            var members = chatRepository.GetMembers(_chat.Id);

            //Assert
            Assert.IsTrue(members.Select(x => x.Id).Contains(_users[2].Id));
        }

        [TestMethod]
        public void ShouldDeleteMember()
        {
            //arrange
            var usersRepository = new UsersRepository(_connectionString);
            var chatRepository = new ChatsRepository(_connectionString, usersRepository);

            //act
            chatRepository.DeleteMember(_chat.Id, _users[1].Id);
            _tempChatIdMemberIds.Remove(_tempChatIdMemberIds.Find(x=>x.ChatId == _chat.Id && x.MemberId == _users[1].Id));
            var members = chatRepository.GetMembers(_chat.Id);

            //Assert
            Assert.IsFalse(members.Select(x => x.Id).Contains(_users[1].Id));
        }

        [TestMethod]
        public void ShouldCreateChat()
        {
            //arrange
            var usersRepository = new UsersRepository(_connectionString);

            var chatWithOneMember = new Chat
            {
                Name = "ChatName",
                Creater = _users[0],
                Members = new List<User> { _users[0] }
            };

            var chatWithTwoMembers = new Chat
            {
                Name = "ChatName",
                Creater = _users[0],
                Members = new List<User> { _users[0], _users[1] }
            };

            var chatWithThreeMembers = new Chat
            {
                Name = "ChatName",
                Creater = _users[0],
                Members = new List<User> { _users[0], _users[1], _users[2] }
            };

            //act
            var chatsRepository = new ChatsRepository(_connectionString, usersRepository);
            var createdChatWithTwoMembers = chatsRepository.CreateChat(chatWithTwoMembers);
            var createdChatWithThreeMembers = chatsRepository.CreateChat(chatWithThreeMembers);
            _tempChatIds.AddRange(new[] { createdChatWithTwoMembers.Id, createdChatWithThreeMembers.Id });

            foreach (var c in new[] { createdChatWithTwoMembers, createdChatWithThreeMembers })
                foreach (var m in c.Members)
                    _tempChatIdMemberIds.Add(new ChatIdMemberId
                    {
                        ChatId = c.Id,
                        MemberId = m.Id
                    });

            //Assert
            Assert.ThrowsException<ArgumentException>(() => chatsRepository.CreateChat(chatWithOneMember));

            Assert.AreEqual(chatWithTwoMembers.Id, createdChatWithTwoMembers.Id);
            Assert.AreEqual(chatWithTwoMembers.Name, createdChatWithTwoMembers.Name);
            Assert.AreEqual(chatWithTwoMembers.Members.Count(), createdChatWithTwoMembers.Members.Count());

            Assert.AreEqual(chatWithThreeMembers.Id, createdChatWithThreeMembers.Id);
            Assert.AreEqual(chatWithThreeMembers.Name, createdChatWithThreeMembers.Name);
            Assert.AreEqual(chatWithThreeMembers.Members.Count(), createdChatWithThreeMembers.Members.Count());
        }

        [TestMethod]
        public void ShouldDeleteChat()
        {
            //arrange
            var usersRepository = new UsersRepository(_connectionString);
            var chatsRepository = new ChatsRepository(_connectionString, usersRepository);

            //act
            chatsRepository.DeleteChat(_chat.Id);
            _tempChatIds.Remove(_chat.Id);

            //assert
            Assert.ThrowsException<ArgumentException>(() => chatsRepository.GetChatInfo(_chat.Id));
        }

        [TestMethod]
        public void ShouldGetChatInfo()
            {
            //arrange
            var usersRepository = new UsersRepository(_connectionString);
            var chatsRepository = new ChatsRepository(_connectionString, usersRepository);

            //act
            var gottenChat = chatsRepository.GetChatInfo(_chat.Id);

            //Assert
            Assert.AreEqual(_chat.Id, gottenChat.Id);
            Assert.AreEqual(_chat.Name, gottenChat.Name);
            Assert.AreEqual(_chat.Creater.Id, gottenChat.Creater.Id);
        }

        [TestMethod]
        public void ShouldGetMessagesForUser()
        {
            //arrange
            var usersRepository = new UsersRepository(_connectionString);
            var chatsRepository = new ChatsRepository(_connectionString, usersRepository);
            var messagesRepository = new MessagesRepository(_connectionString, usersRepository, chatsRepository);
            
            //act
            var messagesOfUser = messagesRepository.GetMessagesForUser(_chat.Id, _messages[0].User.Id);

            //assert
            Assert.IsNotNull(messagesOfUser);
            Assert.IsTrue(messagesOfUser.Select(x => x.Id).Contains(_messages[0].Id));
        }

        [TestMethod]
        public void ShouldGetMembers()
        {
            //arrange
            var usersRepository = new UsersRepository(_connectionString);
            var chatsRepository = new ChatsRepository(_connectionString, usersRepository);

            //act
            var members = chatsRepository.GetMembers(_chat.Id);

            //assert
            Assert.AreEqual(members.Count(), _chat.Members.Count());
        }

        [TestMethod] 
        public void ShouldGetChat()
        {
            //arrange
            var usersRepository = new UsersRepository(_connectionString);
            var chatsRepository = new ChatsRepository(_connectionString, usersRepository);

            //act
            var gottenChat = chatsRepository.GetChat(_chat.Id, _chat.Creater.Id);

            //assert
            Assert.AreEqual(_chat.Members.Count(), gottenChat.Members.Count());
            Assert.AreEqual(_chat.Id, gottenChat.Id);
            Assert.AreEqual(_chat.Name, gottenChat.Name);
            Assert.AreEqual(_chat.Creater.Id, gottenChat.Creater.Id);
            Assert.AreEqual(_chat.Messages.Count(), gottenChat.Messages.Count());
        }

        [TestMethod]
        public void ShouldFindMessagesByPhrase()
        {
            //arrange
            var usersRepository = new UsersRepository(_connectionString);
            var chatsRepository = new ChatsRepository(_connectionString, usersRepository);

            //act
            var messages = chatsRepository.SearchMessagesByPhraseForUser(_chat.Creater.Id, "Message");

            //Assert
            Assert.IsNotNull(messages);
            Assert.IsTrue(messages.All(x => x.Text.Contains("Message")));
        }

        [TestCleanup]
        public void Clean()
        {
            var usersRepository = new UsersRepository(_connectionString);
            var chatsRepository = new ChatsRepository(_connectionString, usersRepository);
            var messagesRepository = new MessagesRepository(_connectionString, usersRepository, chatsRepository);

            foreach (var cm in _tempChatIdMemberIds)
                chatsRepository.DeleteMember(cm.ChatId, cm.MemberId);

            foreach (var m in _tempMessageIds)
                messagesRepository.DeleteMessage(m);

            foreach (var chatId in _tempChatIds)
                chatsRepository.DeleteChat(chatId);

            foreach (var userId in _tempUserIds)
                usersRepository.DeleteUser(userId);
        } 

    }
}
