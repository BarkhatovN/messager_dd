using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Messager.Model;
using Message = Messager.Model.Message;

namespace Messager.WinForms.Controls
{
    public partial class ChatControl : UserControl
    {
        public ChatControl(User user)
        {
            InitializeComponent();
            _user = user;
            _chats = ServiceClient.GetChats(_user.Login).Result;
            _currentChat = (_chats.Length != 0) ? _chats[0] : null;
        }

        private readonly User _user;
        private readonly Chat[] _chats;
        private Chat _currentChat;

        private async void sendBtn_Click(object sender, EventArgs e)
        {
            var msg = messageTxt.Text.Trim();

            byte[] GetPhoto(string filename)
            {
                var converter = new ImageConverter();
                var bitmap = new Bitmap(filename);
                var photo = (byte[]) converter.ConvertTo(bitmap, typeof(byte[]));
                return photo;
            }

            Message message = new Message
            {
                Text = msg,
                User = _user,
                Attachments = (openFileDialog1.FileName != string.Empty)
                    ? new List<byte[]> { GetPhoto(openFileDialog1.FileName) }
                    : null,
                Chat = _currentChat,
                Date = DateTime.UtcNow,
                IsSelfDestructing = destructingChk.Checked
            };

            await ServiceClient.SendMessage(message);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ChatControl_Load(object sender, EventArgs e)
        {
        }


        private Func<Guid, User, Thread> chatUpdater = (chatId, user) =>
        {
            void Start()
            {
                while (true)
                {
                    var msgs = ServiceClient.GetMessages(chatId, user);
                    Task.Delay(1000);
                }
            }

            return new Thread(start: Start);
        };

        private void chatMembersList_SelectedValueChanged(object sender, EventArgs e)
        {
            var index = (sender as ListBox).SelectedIndex;
            _currentChat = _chats[index];

            

            for (int i = 0; i < 10; i++)
                chatPnl.Controls.Add(new MessageControl { Location = new Point(2, i * 75) });
        }
    }
}
