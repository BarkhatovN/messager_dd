using Messager.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Messager.WinForms.Forms
{
    public partial class NewChatDialog : Form
    {
        public NewChatDialog(User[] users, User currentUser)
        {
            InitializeComponent();
            _users = users;
            _currentUser = currentUser;
            checkedListBox1.Items.Clear();
            checkedListBox1.Items.AddRange(users.Where(x=> x.Login != _currentUser.Login)
                .Select(x => $"{x.Login} / {x.FirstName}").ToArray());
        }

        User[] _users;
        User _currentUser;

        private void sendBtn_Click(object sender, EventArgs e)
        {
            var result = new List<User> { _currentUser };

            if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Выберите собеседника");
                return;
            }
            foreach (var checkedItem in checkedListBox1.CheckedItems)
            {
                var tmp = checkedItem.ToString().Split(' ');
                var login = tmp[0];
                var user = _users.Single(x => x.Login == login);

                result.Add(user);
            }

            var chat = new Chat
            {
                Creater = _currentUser,
                Members = result.ToArray(),
                Messages = null,
                Name = chatNameTxt.Text,
            };

            ServiceClient.CreateChat(chat);

            this.Close();
        }
    }
}

