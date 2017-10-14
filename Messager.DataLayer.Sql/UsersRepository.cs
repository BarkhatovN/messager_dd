using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Messager.Model;
using Messager.DataLayer;


namespace Messager.DataLayer.Sql
{
    public class UsersRepository : IUsersRepository
    {
        private readonly string _connectionString;

        public UsersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static string HashSHA1(string password)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(password);
            var hash = sha1.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }


        //AddUser does not work. Cause is 'implicit conversion from nvarchar to varbinary(max)' 
        //I dunno why byte array does not convert to varbinary or binary.
        public User CreateUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    user.Id = Guid.NewGuid();
                    user.Password = HashSHA1(user.Password);
                    if (user.ProfilePhoto != null)
                    {
                        //command.CommandText = "EXECUTE AddUser @FirstName, @LastName, @Photo, @Login, @PasswordHash";
                        command.CommandText = "INSERT INTO Users(Id, FirstName, LastName, Photo, Login, PasswordHash)" +
                                "VALUES(@Id, @FirstName, @LastName, @Photo, @Login, @PasswordHash)";
                        command.Parameters.AddWithValue("@Photo", user.ProfilePhoto);
                    }
                    else
                    {
                        //command.CommandText = "EXECUTE AddUser @FirstName, @LastName, @Login, @PasswordHash";
                        command.CommandText = "INSERT INTO Users(Id, FirstName, LastName, Login, PasswordHash)" +
                              "VALUES(@Id, @FirstName, @LastName, @Login, @PasswordHash)";
                    }
                    command.Parameters.AddWithValue("@Id", user.Id);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Login", user.Login);
                    command.Parameters.AddWithValue("@PasswordHash", user.Password);

                    //user.Id = (Guid)command.ExecuteScalar();
                    command.ExecuteNonQuery();
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
                    command.CommandText = "DELETE FROM Users WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", userId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public User GetUser(String login)
        {
            var encoding = new UTF8Encoding();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "EXECUTE GetUserByLogin @Login";
                    command.Parameters.AddWithValue("@Login", login);
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new ArgumentException($"User with Login: {login} has not been found");
                        return new User
                        {
                            Id = reader.GetGuid(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            ProfilePhoto = reader["Photo"] == DBNull.Value ?
                                null : reader.GetSqlBinary(reader.GetOrdinal("Photo")).Value,
                            Login = reader.GetString(reader.GetOrdinal("Login")),
                            Password = reader.GetString(reader.GetOrdinal("PasswordHash"))
                        };
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
                    command.CommandText = "EXECUTE GetUserById @Id";
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
                            Password = reader.GetString(reader.GetOrdinal("PasswordHash")),
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

                    if (user.ProfilePhoto != null)
                    {
                        command.CommandText = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName," +
                        "Photo = @Photo, Login = @Login, PasswordHash = @PasswordHash WHERE Id = @Id";
                        //command.CommandText = "UpdateUser @Id, @FirstName, @LastName, @Photo, @Login, @PasswordHash";
                        command.Parameters.AddWithValue("@Photo", user.ProfilePhoto);
                    }
                    else
                        //command.CommandText = "UpdateUser @Id, @FirstName, @LastName, @Login, @PasswordHash";
                        command.CommandText = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName," +
                        "Login = @Login, PasswordHash = @PasswordHash WHERE Id = @Id";

                    command.Parameters.AddWithValue("@Id", user.Id);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Login", user.Login);
                    if (user.Password != oldPassword)
                        user.Password = HashSHA1(user.Password);
                    command.Parameters.AddWithValue("@PasswordHash", user.Password);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
