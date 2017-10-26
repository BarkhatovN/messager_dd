using System;
using System.Collections.Generic;
using Messager.Model;

namespace Messager.DataLayer
{
    public interface IMessagesRepository
    {
        Message GetMessage(Guid messageId);
        Message CreateMessage(Message message);
        void DeleteMessage(Guid messageId);
        IEnumerable<Message> GetMessagesForUser(Guid chatId, Guid userId);
    }
}
