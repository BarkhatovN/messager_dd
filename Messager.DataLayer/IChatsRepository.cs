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
        IReadOnlyList<User> GetMembers(Guid chatId);
        void DeleteChat(Guid chatId);
        void AddMember(Guid chatId, Guid userId);
        void DeleteMember(Guid userId, Guid chatId);
        IReadOnlyList<Message> SearchMessagesByPhraseForUser(Guid userId, string phrase);
    }
}
