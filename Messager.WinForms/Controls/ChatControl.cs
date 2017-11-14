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
using Messager.WinForms.Forms;
using Message = Messager.Model.Message;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Messager.WinForms.Controls
{
    public partial class ChatControl : UserControl
    {
        public ChatControl(User user)
        {
            InitializeComponent();
            _profile = user;
            _chats = new List<Chat>();
            _users = ServiceClient.GetUsers();

            if(_profile.ProfilePhoto != null)
                using (var ms = new MemoryStream(_profile.ProfilePhoto))
                {
                    var img = Image.FromStream(ms);
                    img = ResizeImage(img, profilePhotoPB.Size.Width, profilePhotoPB.Size.Height);
                    profilePhotoPB.Image = img;
                }

            chatUpdaterStart();
        }

        private readonly List<byte[]> msgPhotos = new List<byte[]>();
        private readonly User[] _users;
        private readonly User _profile;
        private readonly List<Chat> _chats;
        private Chat _currentChat;
        List<Message> _messages;

        // в методах [item]Updater происходит контроль за новыми чатами/сообщениями
        // при их наличии в них запускается соответсвующий [item]ContainerUpdater
        // и добавляет информацию в контейнер.

        Action messageUpdater;
        Action chatUpdater;
        Action<Message[], Panel> messageContainerUpdater = (messages, container) =>
        {
            var collection = new List<Control>();
            var k = 0;
            if (container.Controls.Count > 0)
                k = container.Controls[container.Controls.Count - 1].Location.Y +
                    container.Controls[container.Controls.Count - 1].Height;

            for (int i = 0; i < messages.Length; i++)
            {
                var msgContainer = new MessageControl { Location = new Point(2, (i * 75) + k), };
                msgContainer.authorTxt.Text = messages[i].User.FirstName + messages[i].User.LastName;
                msgContainer.msgTxt.Text = messages[i].Text;

                collection.Add(msgContainer);

                if (messages[i].Attachments != null)
                    foreach (var attachment in messages[i].Attachments)
                    {
                        using (var ms = new MemoryStream(attachment))
                        {
                            var img = Image.FromStream(ms);
                            img = ResizeImage(img, 150, 150);
                            var pct = new PictureBox() { Image = img, Location = new Point(2, ((i + 1) * 75) + k) };
                            pct.SizeMode = PictureBoxSizeMode.AutoSize;
                            collection.Add(pct);
                            k += img.Height;
                        }
                    }
            }

            if (collection.Count != 0)
                container.Controls.AddRange(collection.ToArray());
        };
        Action<Chat[], Chat, ListBox> chatContainerUpdater = (chats, currentChat, listBox) =>
        {
            listBox.Items.AddRange(chats.Select(x => x.Name).ToArray());
        };

        private void addChatBtn_Click(object sender, EventArgs e)
        {
            var newChatDialog = new NewChatDialog(_users, _profile);
            newChatDialog.ShowDialog();
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            var msg = messageTxt.Text.Trim();

            Message message = new Message
            {
                Text = msg,
                User = _profile,
                Attachments = (msgPhotos.Count != 0)
                    ? msgPhotos
                    : null,
                Chat = _currentChat,
                Date = DateTime.UtcNow,
                IsSelfDestructing = destructingChk.Checked
            };

            ServiceClient.SendMessage(message);
            msgPhotos.Clear();
        }

        private void attachBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Image";
            openFileDialog1.Filter = "jpg files (*.jpeg)|*.jpg";

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            var bitmap = new Bitmap(openFileDialog1.FileName);
            ImageConverter converter = new ImageConverter();

            msgPhotos.Add((byte[])converter.ConvertTo(bitmap, typeof(byte[])));
        }

        private void messageUpdaterStart(Guid chatId, Guid userId, Panel msgContainer)
        {
            messageUpdater = () =>
            {
                while (true)
                {
                    var messages = ServiceClient.GetMessages(chatId, userId);
                    var newMessages = messages.Where(x=>!_messages.Any(y=>y.Id == x.Id)).ToArray();
                    _messages.AddRange(newMessages);

                    if (msgContainer.InvokeRequired && msgContainer.IsHandleCreated && newMessages.Length > 0)
                    {
                        msgContainer.BeginInvoke(messageContainerUpdater, newMessages, msgContainer);
                    }

                    Thread.Sleep(10000);
                }
            };

            messageUpdater.BeginInvoke(null, null);
        }

        private void chatUpdaterStart()
        {
            chatUpdater = () =>
            {
                while (true)
                {
                    var chats = ServiceClient.GetChats(_profile.Id);

                    var newChats = chats.Where(x => !_chats.Any(y => y.Id == x.Id)).ToArray();
                    _chats.AddRange(newChats);

                    if (chatList.InvokeRequired && chatList.IsHandleCreated && newChats.Length > 0)
                        chatList.BeginInvoke(chatContainerUpdater, newChats, _currentChat, chatList);

                    Thread.Sleep(10000);
                }
            };

            chatUpdater.BeginInvoke(null, null);
        }

        private void chatList_SelectedItemChanged(object sender, EventArgs e)
        {
            if(_chats.Count != 0)
                _currentChat = _chats.Single(x => x.Name == chatList.SelectedItem.ToString());

            msgPnl.Controls.Clear();
            msgPnl.Update();
            _messages = new List<Message>();
            messageUpdaterStart(_currentChat.Id, _profile.Id, msgPnl);
        }

        public static Image ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
