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

        private readonly List<byte[]> photos = new List<byte[]>();
        private readonly User[] _users;
        private readonly User _profile;
        private readonly List<Chat> _chats;
        private Chat _currentChat;

        private void sendBtn_Click(object sender, EventArgs e)
        {
            var msg = messageTxt.Text.Trim();

            byte[] GetPhoto(string filename)
            {
                var converter = new ImageConverter();
                var bitmap = new Bitmap(filename);
                var photo = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
                return photo;
            }

            Message message = new Message
            {
                Text = msg,
                User = _profile,
                Attachments = (photos.Count != 0)
                    ? photos
                    : null,
                Chat = _currentChat,
                Date = DateTime.UtcNow,
                IsSelfDestructing = destructingChk.Checked
            };

            ServiceClient.SendMessage(message);
            photos.Clear();
        }

        private void attachBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Image";
            openFileDialog1.Filter = "jpg files (*.jpeg)|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            var bitmap = new Bitmap(openFileDialog1.FileName);
            ImageConverter converter = new ImageConverter();

            photos.Add((byte[])converter.ConvertTo(bitmap, typeof(byte[])));
        }

        private void ChatControl_Load(object sender, EventArgs e)
        {
        }


        Action<Message[], Panel> messageContainerUpdater = (messages, container) =>
        {
            container.Controls.Clear();

            var collection = new List<Control>();
            var k = 0;

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

            container.Controls.Clear();
            container.Controls.AddRange(collection.ToArray());
            collection.Clear();
            container.Refresh();
        };

        Action messageUpdater;

        private void messageUpdaterStart(Guid chatId, Guid userId, Panel msgContainer)
        {
            messageUpdater = () =>
            {
                while (true)
                {
                    var messages = ServiceClient.GetMessages(chatId, userId);

                    if (msgContainer.InvokeRequired && msgContainer.IsHandleCreated)
                        msgContainer.BeginInvoke(messageContainerUpdater, messages.ToArray(), msgContainer);

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
                    _chats.Clear();
                    _chats.AddRange(chats);
                    if (chatList.InvokeRequired && chatList.IsHandleCreated)
                        chatList.BeginInvoke(chatMembersUpdater, _chats, _currentChat, chatList);

                    Thread.Sleep(10000);
                }
            };

            chatUpdater.BeginInvoke(null, null);
        }

        Action chatUpdater;

        Action<List<Chat>,Chat, ListBox> chatMembersUpdater = (chats, currentChat, listBox) =>
         {
             listBox.Items.Clear();
             listBox.Items.AddRange(chats.Select(x => x.Name).ToArray());
             if(currentChat != null)
                listBox.SetSelected(listBox.Items.IndexOf(currentChat.Name), listBox.Items.Contains(currentChat.Name));
             listBox.Refresh();
         };
        private static int k;

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

        private void addChatBtn_Click(object sender, EventArgs e)
        {
            var newChatDialog = new NewChatDialog(_users, _profile);
            newChatDialog.ShowDialog();

        }

        private void chatList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(_chats.Count != 0)
                _currentChat = _chats.Single(x => x.Name == chatList.SelectedItem.ToString());

            messageUpdaterStart(_currentChat.Id, _profile.Id, msgPnl);
        }
    }
}
