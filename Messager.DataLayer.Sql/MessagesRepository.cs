using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Messager.Model;
using Messager.DataLayer;

namespace Messager.DataLayer.Sql
{
    public class MessagesRepository : IMessagesRepository
    {

        private readonly String _connectionString;
        private IUsersRepository _usersRepository;
        private IChatsRepository _chatsRepository;

        public MessagesRepository(String connectionString, IUsersRepository usersRepository, IChatsRepository chatsRepository)
        {
            _connectionString = connectionString;
            _usersRepository = usersRepository;
            _chatsRepository = chatsRepository;
        }

        public Message CreateMessage(Message message)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.Transaction = transaction;
                        command.CommandText = "AddMessage";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ChatId", message.Chat.Id);
                        command.Parameters.AddWithValue("@UserId", message.User.Id);
                        command.Parameters.AddWithValue("@Date", message.Date);
                        command.Parameters.AddWithValue("@Text", message.Text);
                        command.Parameters.AddWithValue("@IsSelfDestructing", message.IsSelfDestructing);

                        message.Id = (Guid)command.ExecuteScalar();
                    }

                    if (message.Attachments != null)
                        foreach (var attachment in message.Attachments)
                        {
                            using (var command = connection.CreateCommand())
                            {
                                command.Transaction = transaction;
                                command.CommandText = "AddAttachment";
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@MessageId", message.Id);
                                command.Parameters.AddWithValue("@File", attachment);

                                command.ExecuteNonQuery();
                            }
                        }

                    transaction.Commit();
                    return message;
                }
            }
        }

        public void DeleteMessage(Guid messageId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Messages WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", messageId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Message GetMessage(Guid messageId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetMessage";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MessageId", messageId);
                    var reader = command.ExecuteReader();
                    if (!reader.HasRows)
                        throw new ArgumentException($"Message with Id: {messageId} has not been found");
                    else
                    {
                        reader.Read();
                        var message = new Message
                        {
                            Id = reader.GetGuid(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            IsSelfDestructing = reader.GetBoolean(reader.GetOrdinal("IsSelfDestructing")),
                            Text = reader.GetString(reader.GetOrdinal("Text")),
                            Chat = _chatsRepository.GetChatInfo(reader.GetGuid(reader.GetOrdinal("ChatId"))),
                            User = _usersRepository.GetUser(reader.GetGuid(reader.GetOrdinal("UserId"))),
                        };

                        message.Attachments = reader["File"] == DBNull.Value ?
                            null : new List<byte[]> { reader.GetSqlBinary(reader.GetOrdinal("File")).Value};

                        while (reader.Read())
                        {
                            if(reader["File"] != DBNull.Value)
                                message.Attachments.Add(reader.GetSqlBinary(reader.GetOrdinal("File")).Value);
                        }
                        

                        return message;
                    }
                }
            }
        }


        public IEnumerable<Message> GetMessagesForUser(Guid chatId, Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var user = _usersRepository.GetUser(userId);
                var chat = _chatsRepository.GetChatInfo(chatId);
                connection.Open();
                ICollection<Message> messages = new List<Message>();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetMessagesForUser";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@ChatId", chatId);

                    using (var reader = command.ExecuteReader())
                    {
                        Message prevMessage = null;
                        Guid prevId = Guid.NewGuid();
                        var result = new List<Message>();
                        while (reader.Read())
                        {
                            var message = new Message
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                IsSelfDestructing = reader.GetBoolean(reader.GetOrdinal("IsSelfDestructing")),
                                Text = reader.GetString(reader.GetOrdinal("Text")),
                                Chat = chat,
                                User = user,
                            };

                            if (prevId != message.Id)
                            {
                                message.Attachments = (reader["File"] == DBNull.Value ?
                                    null : new List<byte[]> { reader.GetSqlBinary(reader.GetOrdinal("File")).Value });

                                if (prevMessage != null)
                                {
                                    result.Add(prevMessage);
                                }
                                prevMessage = message;
                                prevId = message.Id;

                            }
                            else
                            {
                                prevMessage.Attachments.Add(reader.GetSqlBinary(reader.GetOrdinal("File")).Value);
                            }
                        }
                        if (prevMessage == null)
                            return null;

                        result.Add(prevMessage);

                        result.FindAll(m => m.IsSelfDestructing && m.User.Id == userId).ForEach((m) => DeleteMessage(m.Id));
                        return result;
                    }
                }
            }
        }

    }
}
