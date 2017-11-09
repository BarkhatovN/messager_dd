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

namespace Messager.WinForms
{
    static class ServiceClient
    {
        private static HttpClient _client;

        public static void Initialize()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(@"http://localhost:12345/api/")
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

        public static async Task<User> RegisterUser(User user)
        {
            var response = (await _client.PostAsJsonAsync(@"users/register", user)).Content;

            try
            {
                user = response.ReadAsAsync<User>().Result;
                return user;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return new User();
        }
    }
}
