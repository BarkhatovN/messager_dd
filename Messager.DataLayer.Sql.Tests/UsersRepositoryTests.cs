using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Drawing;
using Messager.Model;
using System.Linq;

namespace Messager.DataLayer.Sql.Tests
{
    [TestClass]
    public class UsersRepositoryTests
    {
        private readonly string ConnectionString = @"Server=POTM-PC\SQLEXPRESS;Database=messager_db;User Id=potm2;Password=12312322;";

        private readonly List<Guid> _tempUsers = new List<Guid>();

        [TestMethod]
        public void ShouldCreateUser()
        {
            //arrange
            var user1 = new User
            {
                FirstName = "Thomas",
                LastName = "Anderson",
                Login = "Neo",
                Password = "123123123"
            };


            var photo = Properties.Photos.AgentSmithPhoto;
            ImageConverter converter = new ImageConverter();
            var photoBytes = (byte[])converter.ConvertTo(photo, typeof(byte[]));

            var user2 = new User
            {
                FirstName = "Smith",
                LastName = "Smith",
                Login = "Agent Smith",
                Password = "123123123",
                ProfilePhoto = photoBytes
            };

            //act
            var repository = new UsersRepository(ConnectionString);
            var createdUser1 = repository.CreateUser(user1);
            var createdUser2 = repository.CreateUser(user2);

            _tempUsers.AddRange(new[] { createdUser1.Id, createdUser2.Id });

            //Asserts
            Assert.AreEqual(user1.FirstName, createdUser1.FirstName);
            Assert.AreEqual(user1.LastName, createdUser1.LastName);
            Assert.AreEqual(user1.Login, createdUser1.Login);
            Assert.AreEqual(user1.Password, createdUser1.Password);


            Assert.AreEqual(user2.FirstName, createdUser2.FirstName);
            Assert.AreEqual(user2.LastName, createdUser2.LastName);
            Assert.AreEqual(user2.Login, createdUser2.Login);
            Assert.AreEqual(user2.Password, createdUser2.Password);
            Assert.IsTrue(Enumerable.SequenceEqual(user2.ProfilePhoto, createdUser2.ProfilePhoto));
        }

        [TestMethod]
        public void ShouldDeleteUser()
        {
            //arrange
            var user = new User
            {
                FirstName = "Thomas",
                LastName = "Anderson",
                Login = "Neo1",
                Password = "123123123"
            };
            var repository = new UsersRepository(ConnectionString);
            var result = repository.CreateUser(user);

            //act
            repository.DeleteUser(result.Id);

            //Asserts
            Assert.ThrowsException<ArgumentException>(() => repository.GetUser(result.Id));
        }

        [TestMethod]
        public void ShouldGetUserById()
        {
            //arrange
            var photo = Properties.Photos.AgentSmithPhoto;
            ImageConverter converter = new ImageConverter();
            var photoBytes = (byte[])converter.ConvertTo(photo, typeof(byte[]));

            var user = new User
            {
                FirstName = "Smith",
                LastName = "Smith",
                Login = "Agent Smith",
                Password = "123123123",
                ProfilePhoto = photoBytes
            };

            var userWithoutPhoto = new User
            {
                FirstName = "Smith",
                LastName = "Smith",
                Login = "Agent Smith2",
                Password = "123123123",
                ProfilePhoto = null
            };

            var repository = new UsersRepository(ConnectionString);
            var createdUser = repository.CreateUser(user);
            var createdUserWithoutPhoto = repository.CreateUser(userWithoutPhoto);

            _tempUsers.AddRange(new[] { createdUser.Id, createdUserWithoutPhoto.Id });

            //act
            var gottenUser = repository.GetUser(createdUser.Id);
            var gottenUserWithoutPhoto = repository.GetUser(createdUserWithoutPhoto.Id);

            //Asserts
            Assert.AreEqual(createdUser.Id, gottenUser.Id);
            Assert.AreEqual(createdUser.FirstName, gottenUser.FirstName);
            Assert.AreEqual(createdUser.LastName, gottenUser.LastName);
            Assert.AreEqual(createdUser.Login, gottenUser.Login);
            Assert.AreEqual(createdUser.Password, gottenUser.Password);
            Assert.IsTrue(createdUser.ProfilePhoto.SequenceEqual(gottenUser.ProfilePhoto));

            Assert.AreEqual(createdUserWithoutPhoto.Id, gottenUserWithoutPhoto.Id);
            Assert.AreEqual(createdUserWithoutPhoto.FirstName, gottenUserWithoutPhoto.FirstName);
            Assert.AreEqual(createdUserWithoutPhoto.LastName, gottenUserWithoutPhoto.LastName);
            Assert.AreEqual(createdUserWithoutPhoto.Login, gottenUserWithoutPhoto.Login);
            Assert.AreEqual(createdUserWithoutPhoto.Password, gottenUserWithoutPhoto.Password);
            Assert.AreEqual(createdUserWithoutPhoto.ProfilePhoto, gottenUserWithoutPhoto.ProfilePhoto);
        }

        [TestMethod]
        public void ShouldGetUserByLogin()
        {
            //arrange
            var photo = Properties.Photos.AgentSmithPhoto;
            ImageConverter converter = new ImageConverter();
            var photoBytes = (byte[])converter.ConvertTo(photo, typeof(byte[]));

            var user = new User
            {
                FirstName = "Smith",
                LastName = "Smith",
                Login = "Agent Smith",
                Password = "123123123",
                ProfilePhoto = photoBytes
            };

            var userWithoutPhoto = new User
            {
                FirstName = "Smith",
                LastName = "Smith",
                Login = "Agent Smith2",
                Password = "123123123",
                ProfilePhoto = null
            };

            var repository = new UsersRepository(ConnectionString);
            var createdUser = repository.CreateUser(user);
            var createdUserWithoutPhoto = repository.CreateUser(userWithoutPhoto);
            _tempUsers.AddRange(new[] { createdUser.Id, createdUserWithoutPhoto.Id });

            //act
            var gottenUser = repository.GetUser(createdUser.Login);
            var gottenUserWithoutPhoto = repository.GetUser(createdUserWithoutPhoto.Login);

            //Asserts
            Assert.AreEqual(createdUser.Id, gottenUser.Id);
            Assert.AreEqual(createdUser.FirstName, gottenUser.FirstName);
            Assert.AreEqual(createdUser.LastName, gottenUser.LastName);
            Assert.AreEqual(createdUser.Login, gottenUser.Login);
            Assert.AreEqual(createdUser.Password, gottenUser.Password);
            Assert.IsTrue(createdUser.ProfilePhoto.SequenceEqual(gottenUser.ProfilePhoto));

            Assert.AreEqual(createdUserWithoutPhoto.Id, gottenUserWithoutPhoto.Id);
            Assert.AreEqual(createdUserWithoutPhoto.FirstName, gottenUserWithoutPhoto.FirstName);
            Assert.AreEqual(createdUserWithoutPhoto.LastName, gottenUserWithoutPhoto.LastName);
            Assert.AreEqual(createdUserWithoutPhoto.Login, gottenUserWithoutPhoto.Login);
            Assert.AreEqual(createdUserWithoutPhoto.Password, gottenUserWithoutPhoto.Password);
            Assert.AreEqual(createdUserWithoutPhoto.ProfilePhoto, gottenUserWithoutPhoto.ProfilePhoto);
        }

        [TestMethod]
        public void ShouldUpdateUser()
        {
            //arrange
            var user = new User
            {
                FirstName = "Thomas",
                LastName = "Anderson",
                Login = "Neo2",
                Password = "123123123"
            };

            var repository = new UsersRepository(ConnectionString);
            user = repository.CreateUser(user);
            _tempUsers.Add(user.Id);

            var photo = Properties.Photos.AgentSmithPhoto;
            ImageConverter converter = new ImageConverter();
            var photoBytes = (byte[])converter.ConvertTo(photo, typeof(byte[]));

            var newUserData = new User
            {
                Id = user.Id,
                FirstName = "Neo",
                LastName = "Neo",
                Login = "NewNeo",
                Password = "321321321",
                ProfilePhoto = photoBytes
            };

            //act
            repository.UpdateUser(newUserData);
            var updatedUser = repository.GetUser(newUserData.Id);

            //Asserts
            Assert.AreEqual(newUserData.Id, updatedUser.Id);
            Assert.AreEqual(newUserData.FirstName, updatedUser.FirstName);
            Assert.AreEqual(newUserData.LastName, updatedUser.LastName);
            Assert.AreEqual(newUserData.Login, updatedUser.Login);
            Assert.AreEqual(newUserData.Password, updatedUser.Password);

            if (newUserData.ProfilePhoto != null)
                Assert.IsTrue(newUserData.ProfilePhoto.SequenceEqual(updatedUser.ProfilePhoto));
            else
                Assert.AreEqual(newUserData.ProfilePhoto, updatedUser.ProfilePhoto);
        }

        [TestCleanup]
        public void Clean()
        {
            foreach (var id in _tempUsers)
                new UsersRepository(ConnectionString).DeleteUser(id);
        }
        

    }
}
