using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Messager.Model;
using Messager.DataLayer;
using Messager.DataLayer.Sql;

namespace Messager.Api.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUsersRepository _usersRepository;
        private readonly String ConnectionString = Properties.Settings.Default.ConnectionString;

        public UsersController()
        {
            _usersRepository = new UsersRepository(ConnectionString);
        }

        [HttpGet]
        [Route("api/users/{id}")]
        public User GetById(Guid id)
        {
            return _usersRepository.GetUser(id);
        }

        [HttpGet]
        [Route("api/users/{Login}")]
        public User GetByLogin(string login)
        {
            return _usersRepository.GetUser(login);
        }

        [HttpPost]
        [Route("api/users")]
        public User Create([FromBody] User user)
        {
            return _usersRepository.CreateUser(user);
        }

        [HttpPut]
        [Route("api/users/{id}")]
        public void Update([FromBody] User user)
        {
            _usersRepository.UpdateUser(user);
            return;
        }

        [HttpDelete]
        [Route("api/users/{id}")]
        public void Delete(Guid id)
        {
            _usersRepository.DeleteUser(id);
        }
    }
}
