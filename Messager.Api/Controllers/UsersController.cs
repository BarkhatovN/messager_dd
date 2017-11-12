using System;
using System.Web.Http;
using Messager.Api.Properties;
using Messager.DataLayer;
using Messager.DataLayer.Sql;
using Messager.Model;


namespace Messager.Api.Controllers
{
    public class UsersController : ApiController
    {

        private readonly NLog.ILogger _logger = Logger.Logger.Instance;
        private readonly IUsersRepository _usersRepository;
        private readonly string _connectionString = Settings.Default.ConnectionString;

        public UsersController()
        {
            _usersRepository = new UsersRepository(_connectionString);
        }

        [HttpGet]
        [Route("api/users/all")]
        public User[] GetUsers()
        {
            var users = _usersRepository.GetUsers();
            return users;
        }

        [HttpPost]
        [Route("api/users/login")]
        public User GetByLogin([FromBody]User user)
        {
            _logger.Info($"{DateTime.Now.ToShortDateString()} User with Login: {user.Login} has been queried");
            return _usersRepository.GetUser(user.Login, user.Password);
        }

        [HttpPost]
        [Route("api/users/register")]
        public User Create([FromBody] User user)
        {
            var createdUser = _usersRepository.CreateUser(user);
            _logger.Info($"{DateTime.Now.ToShortDateString()} User with Id: {createdUser.Id} has been created");
            return createdUser;
        }

        [HttpPut]
        [Route("api/users/{id}")]
        public void Update([FromBody] User user)
        {
            _logger.Info($"{DateTime.Now.ToShortDateString()} User with Id: {user.Id} has been updated");
            _usersRepository.UpdateUser(user);
        }

        [HttpDelete]
        [Route("api/users/{id}")]
        public void Delete(Guid id)
        {
            _logger.Info($"{DateTime.Now.ToShortDateString()} User with Id: {id} has been deleted");
            _usersRepository.DeleteUser(id);
        }
    }
}
