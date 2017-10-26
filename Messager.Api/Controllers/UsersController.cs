using System;
using System.Web.Http;
using Messager.Api.Properties;
using Messager.DataLayer;
using Messager.DataLayer.Sql;
using Messager.Model;
using NLog;


namespace Messager.Api.Controllers
{
    public class UsersController : ApiController
    {

        private ILogger logger;
        private readonly IUsersRepository _usersRepository;
        private readonly string _connectionString = Settings.Default.ConnectionString;

        public UsersController()
        {
            _usersRepository = new UsersRepository(_connectionString);
        }

        [HttpGet]
        [Route("api/users/{id}")]
        public User GetById(Guid id)
        {
            logger.Info($"{DateTime.Now.ToShortDateString()} Был получен запрос на пользователь с Id: {id}");
            try
            {
                return _usersRepository.GetUser(id);
            }
        }

        [HttpGet]
        [Route("api/users/{Login}")]
        public User GetByLogin(string login)
        {
            logger.Info($"{DateTime.Now.ToShortDateString()} Пользователь с Id: {login} был выдан");
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
        }

        [HttpDelete]
        [Route("api/users/{id}")]
        public void Delete(Guid id)
        {
            _usersRepository.DeleteUser(id);
        }
    }
}
