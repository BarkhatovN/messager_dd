using System;
using Messager.Model;

namespace Messager.DataLayer
{
    public interface IUsersRepository
    {
        User CreateUser(User user);
        User GetUser(Guid userId);
        User GetUser(string login, string password);
        void DeleteUser(Guid userId);
        void UpdateUser(User user);
    }
}
