using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Messager.DataLayer.Sql;
using Messager.Model;
using Messager.DataLayer;
using System.Text;

namespace Messager.DataLayer.Sql.Tests
{
    [TestClass]
    public class ChatsRepositoryTests
    {
        public struct ChatIdMemberId { public Guid ChatId; public Guid MemberId; }

        private readonly String ConnectionString = @"Server=POTM-PC\SQLEXPRESS;Database=messager_db;User Id=potm2;Password=12312322;";

        private List<Guid> _tempChatIds = new List<Guid>();
        private List<Guid> _tempUserIds = new List<Guid>();
        private List<Guid> _tempMessageIds = new List<Guid>();
        private List<ChatIdMemberId> _tempChatIdMemberIds = new List<ChatIdMemberId>();

        private List<User> Users;
        private Chat Chat;
        private List<Message> Messages;

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
            _tempUserIds.AddRange(Users.Select(u=>u.Id));
            
            var chat = new Chat
            {
                Creater = Users[0],
                Members = new[] { Users[0], Users[1] },
                Name = "ChatName"
            };

            var chatsRepository = new ChatsRepository(ConnectionString, usersRepository);
            Chat = chatsRepository.CreateChat(chat);
            _tempChatIds.Add(Chat.Id);
            _tempChatIdMemberIds.AddRange(new[]
            {
                new ChatIdMemberId{ ChatId = Chat.Id, MemberId = Users[0].Id },
                new ChatIdMemberId{ ChatId = Chat.Id, MemberId = Users[1].Id }
            });

            var message1 = new Message
            {
                Chat = Chat,
                Date = DateTime.Now.ToUniversalTime(),
                IsSelfDestructing = false,
                Text = "Message1",
                User = Users[0],
                Attachments = new[]
                {
                    Encoding.UTF8.GetBytes("Hello"),
                    Encoding.UTF8.GetBytes("World")
                }
            };

            var message2 = new Message
            {
                Chat = Chat,
                Date = DateTime.Now.ToUniversalTime(),
                IsSelfDestructing = false,
                Text = "Message2",
                User = Users[0]
            };

            var messagesRepository = new MessagesRepository(ConnectionString, usersRepository, chatsRepository);
            Messages = new List<Message> 
            {
                messagesRepository.CreateMessage(message1),
                messagesRepository.CreateMessage(message2)
            };
            _tempMessageIds.AddRange(Messages.Select(m => m.Id));
            Chat.Messages = Messages;
        }

        [TestMethod]
        public void ShouldAddMember()
        {
            //arrange
            var usersRepository = new UsersRepository(ConnectionString);
            var chatRepository = new ChatsRepository(ConnectionString, usersRepository);

            //act
            chatRepository.AddMember(Chat.Id, Users[2].Id);
            _tempChatIdMemberIds.Add(new ChatIdMemberId { ChatId = Chat.Id, MemberId = Users[2].Id });
            var members = chatRepository.GetMembers(Chat.Id);

            //Assert
            Assert.IsTrue(members.Select(x => x.Id).Contains(Users[2].Id));
        }

        [TestMethod]
        public void ShouldDeleteMember()
        {
            //arrange
            var usersRepository = new UsersRepository(ConnectionString);
            var chatRepository = new ChatsRepository(ConnectionString, usersRepository);

            //act
            chatRepository.DeleteMember(Chat.Id, Users[1].Id);
            _tempChatIdMemberIds.Remove(_tempChatIdMemberIds.Find(x=>x.ChatId == Chat.Id && x.MemberId == Users[1].Id));
            var members = chatRepository.GetMembers(Chat.Id);

            //Assert
            Assert.IsFalse(members.Select(x => x.Id).Contains(Users[1].Id));
 
        }

        [TestMethod]
        public void ShouldCreateChat()
        {
            //arrange
            var usersRepository = new UsersRepository(ConnectionString);

            var chatWithOneMember = new Chat
            {
                Name = "ChatName",
                Creater = Users[0],
                Members = new List<User> { Users[0] }
            };

            var chatWithTwoMembers = new Chat
            {
                Name = "ChatName",
                Creater = Users[0],
                Members = new List<User> { Users[0], Users[1] }
            };

            var chatWithThreeMembers = new Chat
            {
                Name = "ChatName",
                Creater = Users[0],
                Members = new List<User> { Users[0], Users[1], Users[2] }
            };

            //act
            var chatsRepository = new ChatsRepository(ConnectionString, usersRepository);
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
            var usersRepository = new UsersRepository(ConnectionString);
            var chatsRepository = new ChatsRepository(ConnectionString, usersRepository);

            //act
            chatsRepository.DeleteChat(Chat.Id);
            _tempChatIds.Remove(Chat.Id);

            //assert
            Assert.ThrowsException<ArgumentException>(() => chatsRepository.GetChatInfo(Chat.Id));
        }

        [TestMethod]
        public void ShouldGetChatInfo()
            {
            //arrange
            var usersRepository = new UsersRepository(ConnectionString);
            var chatsRepository = new ChatsRepository(ConnectionString, usersRepository);

            //act
            var gottenChat = chatsRepository.GetChatInfo(Chat.Id);

            //Assert
            Assert.AreEqual(Chat.Id, gottenChat.Id);
            Assert.AreEqual(Chat.Name, gottenChat.Name);
            Assert.AreEqual(Chat.Creater.Id, gottenChat.Creater.Id);
        }

        [TestMethod]
        public void ShouldGetMessagesForUser()
        {
            //arrange
            var usersRepository = new UsersRepository(ConnectionString);
            var chatsRepository = new ChatsRepository(ConnectionString, usersRepository);
            var messagesRepository = new MessagesRepository(ConnectionString, usersRepository, chatsRepository);
            
            //act
            var messagesOfUser = chatsRepository.GetMessagesForUser(Chat.Id, Messages[0].User.Id);

            //assert
            Assert.IsNotNull(messagesOfUser);
            Assert.IsTrue(messagesOfUser.Select(x => x.Id).Contains(Messages[0].Id));
        }

        [TestMethod]
        public void ShouldGetMembers()
        {
            //arrange
            var usersRepository = new UsersRepository(ConnectionString);
            var chatsRepository = new ChatsRepository(ConnectionString, usersRepository);

            //act
            var members = chatsRepository.GetMembers(Chat.Id);

            //assert
            Assert.AreEqual(members.Count(), Chat.Members.Count());
        }

        [TestMethod] 
        public void ShouldGetChat()
        {
            //arrange
            var usersRepository = new UsersRepository(ConnectionString);
            var chatsRepository = new ChatsRepository(ConnectionString, usersRepository);

            //act
            var gottenChat = chatsRepository.GetChat(Chat.Id, Chat.Creater.Id);

            //assert
            Assert.AreEqual(Chat.Members.Count(), gottenChat.Members.Count());
            Assert.AreEqual(Chat.Id, gottenChat.Id);
            Assert.AreEqual(Chat.Name, gottenChat.Name);
            Assert.AreEqual(Chat.Creater.Id, gottenChat.Creater.Id);
            Assert.AreEqual(Chat.Messages.Count(), gottenChat.Messages.Count());
        }

        [TestMethod]
        public void ShouldFindMessagesByPhrase()
        {
            //arrange
            var usersRepository = new UsersRepository(ConnectionString);
            var chatsRepository = new ChatsRepository(ConnectionString, usersRepository);

            //act
            var messages = chatsRepository.SearchMessagesByPhraseForUser(Chat.Creater.Id, "Message");

            //Assert
            Assert.IsNotNull(messages);
            Assert.IsTrue(messages.All(x => x.Text.Contains("Message")));
        }

        [TestCleanup]
        public void Clean()
        {
            var usersRepository = new UsersRepository(ConnectionString);
            var chatsRepository = new ChatsRepository(ConnectionString, usersRepository);
            var messagesRepository = new MessagesRepository(ConnectionString, usersRepository, chatsRepository);

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
