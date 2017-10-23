using System;
using Messager.Model;

namespace Messager.DataLayer
{
    public interface IUsersRepository
    {
        User CreateUser(User user);
        User GetUser(Guid userId);
        User GetUser(String login);
        void DeleteUser(Guid userId);
        void UpdateUser(User user);
    }
}
