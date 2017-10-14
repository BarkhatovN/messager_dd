using System;
using Messager.Model;

namespace Messager.DataLayer
{
    public interface IUsersRepository
    {
        User CreateUser(User user);
        User GetUser(Guid userId);
        void DeleteUser(Guid userId);
        void UpdateUser(User user);
    }
}
