﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using Messager.Model;
using Messager.DataLayer;

namespace Messager.DataLayer.Sql
{
    public class ChatsRepository : IChatsRepository
    {
        private readonly string _connectionString;
        private readonly IUsersRepository _usersRepository;

        public ChatsRepository(string connectionString, IUsersRepository usersRepository)
        {
            _connectionString = connectionString;
            _usersRepository = usersRepository;
        }

        public void AddMember(Guid chatId, Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "AddMember";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ChatId", chatId);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Date", DateTime.Now.ToUniversalTime());

                    command.ExecuteNonQuery();
                }
            }
        }

        public Chat CreateChat(Chat chat)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    chat.Id = Guid.NewGuid();

                    using (var command = connection.CreateCommand())
                    {
                        command.Transaction = transaction;
                        command.CommandText = "AddChat";
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", chat.Id);
                        command.Parameters.AddWithValue("@Name", chat.Name);
                        command.Parameters.AddWithValue("@CreaterId", chat.Creater.Id);
                        command.ExecuteNonQuery();
                    }

                    DateTime date = DateTime.Now.ToUniversalTime();

                    if (chat.Members.Count() < 2)
                        throw new ArgumentException($"In chat with Id {chat.Id} count of members less then 2 ({chat.Members.Count()})");

                    var userRepository = new UsersRepository(_connectionString);
                    foreach (var userId in chat.Members.Select(user => user.Id))
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            AddMember(chat.Id, userId);
                        }
                    }
                    transaction.Commit();

                    return chat;
                }
            }
        }

        public void DeleteChat(Guid chatId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DeleteChat";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ChatId", chatId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteMember(Guid chatId, Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DeleteMember";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ChatId", chatId);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Get returns Chat {Id, Creater, Name}.
        /// </summary>
        /// <param name="ChatId"></param>
        /// <returns></returns>
        public Chat GetChatInfo(Guid chatId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetChatInfo";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ChatId", chatId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new ArgumentException($"Chat with Id {chatId} has not been found");

                        return new Chat
                        {
                            Id = reader.GetGuid(reader.GetOrdinal("Id")),
                            Creater = _usersRepository.GetUser(reader.GetGuid(reader.GetOrdinal("CreaterId"))),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };
                    }
                }
            }
        }

        public Chat GetChat(Guid chatId, Guid userId)
        {
            var info = GetChatInfo(chatId);
            return new Chat
            {
                Id = info.Id,
                Name = info.Name,
                Creater = info.Creater,
                Members = GetMembers(chatId),
                Messages = GetMessagesForUser(chatId, userId)
            };
        }

        public IEnumerable<User> GetMembers(Guid chatId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetMembers";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ChatId", chatId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return _usersRepository.GetUser(reader.GetGuid(reader.GetOrdinal("UserId")));
                        }
                    }
                }
            }

        }

        public IEnumerable<Message> GetMessagesForUser(Guid chatId, Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var user = _usersRepository.GetUser(userId);
                var chat = GetChatInfo(chatId);
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
                        return result;
                    }
                }
            }
        }

        public IEnumerable<Message> SearchMessagesByPhraseForUser(Guid userId, String phrase)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var usersRepository = new UsersRepository(_connectionString);
                var chatsRepository = new ChatsRepository(_connectionString, usersRepository);
                var user = _usersRepository.GetUser(userId);
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SearchMessagesByPhrase";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Text", phrase);

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
                                Chat = chatsRepository.GetChatInfo(reader.GetGuid(reader.GetOrdinal("ChatId"))),
                                User = user
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
                        return result;

                    }
                }

            }
        }
    }
}
