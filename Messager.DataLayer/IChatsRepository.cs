using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messager.Model;

namespace Messager.DataLayer
{
    public interface IChatsRepository
    {
        Chat GetChatInfo(Guid ChatId);
        Chat CreateChat(Chat chat);
        IEnumerable<User> GetMembers(Guid chatId);
        void DeleteChat(Guid chatId);
        void AddMember(Guid userId, Guid chatId);
        void DeleteMember(Guid userId, Guid chatId);
    }
}
