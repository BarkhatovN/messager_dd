using System;
using System.Collections.Generic;
using Messager.Model;

namespace Messager.DataLayer
{
    public interface IChatsRepository
    {
        Chat GetChat(Guid id, Guid userId);
        Chat GetChatInfo(Guid chatId);
        Chat CreateChat(Chat chat);
        IEnumerable<User> GetMembers(Guid chatId);
        void DeleteChat(Guid chatId);
        void AddMember(Guid chatId, Guid userId);
        void DeleteMember(Guid userId, Guid chatId);
        IEnumerable<Message> SearchMessagesByPhraseForUser(Guid userId, String phrase);
    }
}
