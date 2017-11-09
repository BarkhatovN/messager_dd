using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using Messager.Model;

namespace Messager.DataLayer.Sql
{
    public class UsersRepository : IUsersRepository
    {
        private readonly string _connectionString;

        public UsersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static string HashSha1(string password)
        {
            var sha1 = SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(password);
            var hash = sha1.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (var t in hash)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }


        public User CreateUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    user.Id = Guid.NewGuid();
                    user.Password = HashSha1(user.Password);
                    
                    command.CommandText = "AddUser";
                    command.CommandType = CommandType.StoredProcedure;
                    if (user.ProfilePhoto != null)
                        command.Parameters.AddWithValue("@Photo", user.ProfilePhoto);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Login", user.Login);
                    command.Parameters.AddWithValue("@PasswordHash", user.Password);
                    user.Id = (Guid)command.ExecuteScalar();
                    return user;
                }
            }
        }

        public void DeleteUser(Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DeleteUser";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", userId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public User GetUser(string login, string password)
        {
            var encoding = new UTF8Encoding();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetUserByLogin";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Login", login);
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new ArgumentException($"User with Login: {login} has not been found");
                        var user =  new User
                        {
                            Id = reader.GetGuid(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            ProfilePhoto = reader["Photo"] == DBNull.Value ?
                                null : reader.GetSqlBinary(reader.GetOrdinal("Photo")).Value,
                            Login = reader.GetString(reader.GetOrdinal("Login")),
                            Password = reader.GetString(reader.GetOrdinal("PasswordHash"))
                        };
                        if (user.Password == password)
                            return user;
                        throw new ArgumentException("User`s password is incorrect");
                    }
                }
            }
        }

        public User GetUser(Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetUserById";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new ArgumentException($"User with Id {userId} has not been found");
                        return new User
                        {
                            Id = reader.GetGuid(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Login = reader.GetString(reader.GetOrdinal("Login")),
                            ProfilePhoto = reader["Photo"] == DBNull.Value ?
                                null : reader.GetSqlBinary(reader.GetOrdinal("Photo")).Value,
                            Password = reader.GetString(reader.GetOrdinal("PasswordHash"))
                        };
                    }
                }
            }
        }

        public void UpdateUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    var oldPassword = GetUser(user.Id).Password;
                    command.CommandText = "UpdateUser";
                    command.CommandType = CommandType.StoredProcedure;

                    if (user.ProfilePhoto != null)
                        command.Parameters.AddWithValue("@Photo", user.ProfilePhoto);
                    
                    command.Parameters.AddWithValue("@Id", user.Id);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Login", user.Login);

                    if (user.Password != oldPassword)
                        user.Password = HashSha1(user.Password);

                    command.Parameters.AddWithValue("@PasswordHash", user.Password);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
