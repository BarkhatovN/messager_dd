using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
