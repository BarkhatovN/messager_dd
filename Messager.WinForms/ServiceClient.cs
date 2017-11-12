using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Messager.Model;
using Message = Messager.Model.Message;

namespace Messager.WinForms
{
    static class ServiceClient
    {
        private static HttpClient _client;

        static ServiceClient()
        {
            Initialize();
        }

        static string HashSha1(string password)
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

        public static void Initialize()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(@"http://localhost:49890/api/")
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static string GetSHA512Hash(string input)
        {
            SHA512 shaM = new SHA512Managed();
            byte[] data = shaM.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            shaM.Dispose();
            return sBuilder.ToString();
        }

        public static User RegisterUser(User user)
        {
            user.Password = HashSha1(user.Password);
            var registeredUser = _client.PostAsJsonAsync(@"users/register", user).Result.Content
                .ReadAsAsync<User>().Result;

            return registeredUser;
        }

        public static User Authorize(string login, string password)
        {
            var user = new User { Login = login, Password = HashSha1(password) };

            var authorizedUser = _client.PostAsJsonAsync(@"users/login", user).Result.Content
                .ReadAsAsync<User>().Result;

            return authorizedUser;
        }

        public static void SendMessage(Message message)
        {
            _client.PostAsJsonAsync<Message>("messages", message);
            return;
        }

        public static Chat CreateChat(Chat chat)
        {
            var addedChat = _client.PostAsJsonAsync<Chat>(@"chats", chat).Result.Content.ReadAsAsync<Chat>().Result;
            return addedChat;
        }

        public static Message[] GetMessages(Guid chatId, Guid userId)
        {
            var messages = _client.GetAsync($"users/{userId}/chats/{chatId}/messages")
                .Result.Content.ReadAsAsync<Message[]>().Result;
            return messages;
        }

        public static Chat[] GetChats(Guid userId)
        {
            var chats =  _client.GetAsync($"chats/{userId}").Result.Content.ReadAsAsync<Chat[]>().Result;
            return chats;
        }

        public static User[] GetUsers()
        {
            var users = _client.GetAsync("users/all").Result.Content.ReadAsAsync<User[]>().Result;
            return users;
        }

        public static void AddChatMember(Chat chat, User user)
        {
            
        }

    }
}
