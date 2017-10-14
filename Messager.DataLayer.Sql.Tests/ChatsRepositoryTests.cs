using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Messager.DataLayer.Sql;
using Messager.Model;
using Messager.DataLayer;

namespace Messager.DataLayer.Sql.Tests
{
    [TestClass]
    public class ChatsRepositoryTests
    {
        private readonly String ConnectionString = @"Server=POTM-PC\SQLEXPRESS;Database=messager_db;User Id=potm2;Password=12312322;";

        private List<Guid> _tempChats = new List<Guid>();
        private List<Guid> _tempMembers = new List<Guid>();

        

        [TestMethod]
        public void ShouldCreateChat()
            {
            //arrange
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

            _tempMembers.AddRange(new[] { createdUser1.Id, createdUser2.Id, createdUser3.Id });

            var chatWithTwoMembers = new Chat
            {
                Name = "ChatName",
                Creater = createdUser1,
                Members = new List<User> { createdUser1, createdUser2 }
            };

            var chatWithThreeMembers = new Chat
            {
                Name = "ChatName",
                Creater = createdUser1,
                Members = new List<User> { createdUser1, createdUser2, createdUser3 }
            };

            //act
            var chatsRepository = new ChatsRepository(ConnectionString, usersRepository);
            var createdChatWithTwoMembers = chatsRepository.CreateChat(chatWithTwoMembers);
            var createdChatWithThreeMembers = chatsRepository.CreateChat(chatWithThreeMembers);
            _tempChats.AddRange(new[] { createdChatWithTwoMembers.Id, createdChatWithThreeMembers.Id });

            //Assert
            Assert.AreEqual(chatWithTwoMembers.Id, createdChatWithTwoMembers.Id);
            Assert.AreEqual(chatWithTwoMembers.Name, createdChatWithTwoMembers.Name);
            Assert.IsTrue(chatWithTwoMembers.Members.SequenceEqual(createdChatWithTwoMembers.Members));

            Assert.AreEqual(chatWithThreeMembers.Id, createdChatWithThreeMembers.Id);
            Assert.AreEqual(chatWithThreeMembers.Name, createdChatWithThreeMembers.Name);
            Assert.IsTrue(chatWithThreeMembers.Members.SequenceEqual(createdChatWithThreeMembers.Members));
        }

        [TestMethod]
        public void ShouldGetChatInfo()
            {
            //arrange
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

            _tempMembers.AddRange(new[] { createdUser1.Id, createdUser2.Id, createdUser3.Id });

            var chat = new Chat
            {
                Name = "ChatName",
                Creater = createdUser1,
                Members = new List<User> { createdUser1, createdUser2, createdUser3 }
            };

            var chatsRepository = new ChatsRepository(ConnectionString, usersRepository);
            var createdChat = chatsRepository.CreateChat(chat);
            _tempChats.Add(createdChat.Id);

            //act
            var gottenChat = chatsRepository.GetChatInfo(createdChat.Id);

            //Assert
            Assert.AreEqual(chat.Id, gottenChat.Id);
            Assert.AreEqual(chat.Name, gottenChat.Name);
            Assert.AreEqual(chat.Creater.Id, gottenChat.Creater.Id);
        }

        [TestMethod]
        public void ShouldDeleteChat()
        {
            //arrange
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

            var usersRepository = new UsersRepository(ConnectionString);
            var createdUser1 = usersRepository.CreateUser(user1);
            var createdUser2 = usersRepository.CreateUser(user2);

            _tempMembers.AddRange(new[] { createdUser1.Id, createdUser2.Id});

            var chat = new Chat
            {
                Name = "ChatName",
                Creater = createdUser1,
                Members = new List<User> { createdUser1, createdUser2 }
            };

            var chatsRepository = new ChatsRepository(ConnectionString, usersRepository);
            var createdChat = chatsRepository.CreateChat(chat);

            //act
            chatsRepository.DeleteChat(createdChat.Id);

            //assert
            Assert.ThrowsException<ArgumentException>(() => chatsRepository.GetChatInfo(createdChat.Id));
        }

        [TestCleanup]
        public void Clean()
        {
            var usersRepository = new UsersRepository(ConnectionString);
            var chatsRepository = new ChatsRepository(ConnectionString, usersRepository);

            foreach (var chatId in _tempChats)
                chatsRepository.DeleteChat(chatId);

            foreach (var userId in _tempMembers)
                usersRepository.DeleteUser(userId);
        } 

    }
}
