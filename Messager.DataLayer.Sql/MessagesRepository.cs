using System;
using System.Collections.Generic;
using System.Linq;
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
                message.Id = Guid.NewGuid();
                using (var transaction = connection.BeginTransaction())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.Transaction = transaction;
                        command.CommandText = "INSERT INTO Messages(Id, ChatId, Text, Date, IsSelfDestructing, UserId)" +
                            "VALUES(@Id, @ChatId, @Text, @Date, @IsSelfDestructing, @UserId)";
                        command.Parameters.AddWithValue("@Id", message.Id);
                        command.Parameters.AddWithValue("@ChatId", message.Chat.Id);
                        command.Parameters.AddWithValue("@UserId", message.User.Id);
                        command.Parameters.AddWithValue("@Date", message.Date);
                        command.Parameters.AddWithValue("@Text", message.Text);
                        command.Parameters.AddWithValue("@IsSelfDestructing", message.IsSelfDestructing);

                        command.ExecuteNonQuery();
                    }

                    foreach (var attachment in message.Attachments)
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandText = "INSERT INTO Files VALUES(@FileId, @File);" +
                                "INSERT INTO Attachments(FileId, MessageId) VALUES(@FileId, @MessageId)";
                            command.Parameters.AddWithValue("@MessageId", message.Id);
                            command.Parameters.AddWithValue("@FileId", Guid.NewGuid());
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
                    command.CommandText = "SELECT * FROM Messages JOIN Attachments ON Messages.Id = Attachments.MessageId " +
                        "JOIN Files ON Attachments.FileId = Files.Id WHERE Messages.Id = @Id";
                    command.Parameters.AddWithValue("@Id", messageId);
                    var reader = command.ExecuteReader();
                    if (!reader.HasRows)
                        throw new ArgumentException($"Message with Id: {messageId} has not been found");
                    else
                    {
                        var message = new Message
                        {
                            Id = reader.GetGuid(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            IsSelfDestructing = reader.GetBoolean(reader.GetOrdinal("IsSelfDestructing")),
                            Text = reader.GetString(reader.GetOrdinal("Text")),
                            Chat = _chatsRepository.GetChatInfo(reader.GetGuid(reader.GetOrdinal("ChatId"))),
                            User = _usersRepository.GetUser(reader.GetGuid(reader.GetOrdinal("UserId")))
                        };

                        message.Attachments.Add(reader["File"] == DBNull.Value ?
                                                null : reader.GetSqlBinary(reader.GetOrdinal("File")).Value);
                        while (reader.Read())
                        {
                            message.Attachments.Add(reader["File"] == DBNull.Value ?
                                                    null : reader.GetSqlBinary(reader.GetOrdinal("File")).Value);
                        }
                        return message;
                    }
                }
            }
        }
    }
}
