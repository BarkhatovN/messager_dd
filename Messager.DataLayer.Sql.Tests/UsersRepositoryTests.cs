using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Messager.DataLayer.Sql.Tests.Properties;
using Messager.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Messager.DataLayer.Sql.Tests
{
    [TestClass]
    public class UsersRepositoryTests
    {
        private readonly string _connectionString = Settings.Default.ConnectionString;

        private readonly List<Guid> _tempUsers = new List<Guid>();

        private User _userWithoutPhoto;
        private User _userWithPhoto;

        [TestInitialize]
        public void Initialize()
        {
            _userWithoutPhoto = new User
            {
                FirstName = "Thomas",
                LastName = "Anderson",
                Login = "Neo",
                Password = "123123123"
            };

            var photo = Photos.AgentSmithPhoto;
            ImageConverter converter = new ImageConverter();
            var photoBytes = (byte[])converter.ConvertTo(photo, typeof(byte[]));

            _userWithPhoto = new User
            {
                FirstName = "Smith",
                LastName = "Smith",
                Login = "Agent Smith",
                Password = "123123123",
                ProfilePhoto = photoBytes
            };
        }

        [TestMethod]
        public void ShouldCreateUser()
        {
            //arrange
            var repository = new UsersRepository(_connectionString);

            //act
            var createdUWithoutPhoto = repository.CreateUser(_userWithoutPhoto);
            var createdUWithPhoto = repository.CreateUser(_userWithPhoto);
            _tempUsers.AddRange(new[] { createdUWithoutPhoto.Id, createdUWithPhoto.Id });

            //Asserts
            Assert.AreEqual(_userWithoutPhoto.FirstName, createdUWithoutPhoto.FirstName);
            Assert.AreEqual(_userWithoutPhoto.LastName, createdUWithoutPhoto.LastName);
            Assert.AreEqual(_userWithoutPhoto.Login, createdUWithoutPhoto.Login);
            Assert.AreEqual(_userWithoutPhoto.Password, createdUWithoutPhoto.Password);


            Assert.AreEqual(_userWithPhoto.FirstName, createdUWithPhoto.FirstName);
            Assert.AreEqual(_userWithPhoto.LastName, createdUWithPhoto.LastName);
            Assert.AreEqual(_userWithPhoto.Login, createdUWithPhoto.Login);
            Assert.AreEqual(_userWithPhoto.Password, createdUWithPhoto.Password);
            Assert.IsTrue(_userWithPhoto.ProfilePhoto.SequenceEqual(createdUWithPhoto.ProfilePhoto));
        }

        [TestMethod]
        public void ShouldDeleteUser()
        {
            //arrange
            var repository = new UsersRepository(_connectionString);
            var createdUser = repository.CreateUser(_userWithoutPhoto);

            //act
            repository.DeleteUser(createdUser.Id);

            //Asserts
            Assert.ThrowsException<ArgumentException>(() => repository.GetUser(createdUser.Id));
        }

        [TestMethod]
        public void ShouldGetUserById()
        {
            //arrange
            var repository = new UsersRepository(_connectionString);
            var createdUWithPhoto = repository.CreateUser(_userWithPhoto);
            var createdUWithoutPhoto = repository.CreateUser(_userWithoutPhoto);

            _tempUsers.AddRange(new[] { createdUWithPhoto.Id, createdUWithoutPhoto.Id });

            //act
            var gottenUWithPhoto = repository.GetUser(createdUWithPhoto.Id);
            var gottenUWithoutPhoto = repository.GetUser(createdUWithoutPhoto.Id);

            //Asserts
            Assert.AreEqual(createdUWithPhoto.Id, gottenUWithPhoto.Id);
            Assert.AreEqual(createdUWithPhoto.FirstName, gottenUWithPhoto.FirstName);
            Assert.AreEqual(createdUWithPhoto.LastName, gottenUWithPhoto.LastName);
            Assert.AreEqual(createdUWithPhoto.Login, gottenUWithPhoto.Login);
            Assert.AreEqual(createdUWithPhoto.Password, gottenUWithPhoto.Password);
            Assert.IsTrue(createdUWithPhoto.ProfilePhoto.SequenceEqual(gottenUWithPhoto.ProfilePhoto));

            Assert.AreEqual(createdUWithoutPhoto.Id, gottenUWithoutPhoto.Id);
            Assert.AreEqual(createdUWithoutPhoto.FirstName, gottenUWithoutPhoto.FirstName);
            Assert.AreEqual(createdUWithoutPhoto.LastName, gottenUWithoutPhoto.LastName);
            Assert.AreEqual(createdUWithoutPhoto.Login, gottenUWithoutPhoto.Login);
            Assert.AreEqual(createdUWithoutPhoto.Password, gottenUWithoutPhoto.Password);
            Assert.IsNull(gottenUWithoutPhoto.ProfilePhoto);
        }

        [TestMethod]
        public void ShouldGetUserByLogin()
        {
            //arrange
            var repository = new UsersRepository(_connectionString);
            var createdUWithPhoto = repository.CreateUser(_userWithPhoto);
            var createdUWithoutPhoto = repository.CreateUser(_userWithoutPhoto);
            _tempUsers.AddRange(new[] { createdUWithPhoto.Id, createdUWithoutPhoto.Id });

            //act
            var gottenUWithPhoto = repository.GetUser(createdUWithPhoto.Login);
            var gottenUWithoutPhoto = repository.GetUser(createdUWithoutPhoto.Login);

            //Asserts
            Assert.AreEqual(createdUWithPhoto.Id, gottenUWithPhoto.Id);
            Assert.AreEqual(createdUWithPhoto.FirstName, gottenUWithPhoto.FirstName);
            Assert.AreEqual(createdUWithPhoto.LastName, gottenUWithPhoto.LastName);
            Assert.AreEqual(createdUWithPhoto.Login, gottenUWithPhoto.Login);
            Assert.AreEqual(createdUWithPhoto.Password, gottenUWithPhoto.Password);
            Assert.IsTrue(createdUWithPhoto.ProfilePhoto.SequenceEqual(gottenUWithPhoto.ProfilePhoto));

            Assert.AreEqual(createdUWithoutPhoto.Id, gottenUWithoutPhoto.Id);
            Assert.AreEqual(createdUWithoutPhoto.FirstName, gottenUWithoutPhoto.FirstName);
            Assert.AreEqual(createdUWithoutPhoto.LastName, gottenUWithoutPhoto.LastName);
            Assert.AreEqual(createdUWithoutPhoto.Login, gottenUWithoutPhoto.Login);
            Assert.AreEqual(createdUWithoutPhoto.Password, gottenUWithoutPhoto.Password);
            Assert.AreEqual(createdUWithoutPhoto.ProfilePhoto, gottenUWithoutPhoto.ProfilePhoto);
        }

        [TestMethod]
        public void ShouldUpdateUser()
        {
            //arrange
            var repository = new UsersRepository(_connectionString);
            var createdUser = repository.CreateUser(_userWithoutPhoto);
            _tempUsers.Add(createdUser.Id);

            var newUserData = _userWithPhoto;
            newUserData.Id = createdUser.Id;

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
                new UsersRepository(_connectionString).DeleteUser(id);
        }
        

    }
}
